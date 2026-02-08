using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Engine.Spatial;
using RealmsOfIdle.Core.Infrastructure;

namespace RealmsOfIdle.Core.Scenarios.Inn.GameLoop;

/// <summary>
/// Main game loop for the Inn scenario
/// </summary>
public sealed class InnGameLoop
{
    private readonly DeterministicRng _rng;
    private readonly CustomerArrivalSystem _arrivalSystem;
    private readonly Dictionary<string, SceneGraph> _areaGraphs;

    /// <summary>
    /// Gets the current game state
    /// </summary>
    public InnState State { get; private set; }

    /// <summary>
    /// Gets the current tick number
    /// </summary>
    public int CurrentTick { get; private set; }

    /// <summary>
    /// Initializes a new instance of InnGameLoop
    /// </summary>
    public InnGameLoop(InnState initialState, DeterministicRng rng)
    {
        ArgumentNullException.ThrowIfNull(initialState);
        ArgumentNullException.ThrowIfNull(rng);

        State = initialState;
        _rng = rng;
        CurrentTick = 0;

        var config = new ArrivalConfig();
        _arrivalSystem = new CustomerArrivalSystem(config, rng.WithOffset(1000));
        _arrivalSystem.CalculateNextArrival(State.Reputation);

        _areaGraphs = new Dictionary<string, SceneGraph>();
        BuildAreaGraphs();
    }

    /// <summary>
    /// Processes a single tick of game logic
    /// </summary>
    public void ProcessTick()
    {
        CurrentTick++;

        // Process arrivals
        _arrivalSystem.ProcessTick();
        if (_arrivalSystem.ShouldArrive && _arrivalSystem.CanArrive(State.Customers.Count))
        {
            var customer = _arrivalSystem.GenerateCustomer();
            State = State.AddCustomer(customer);
            _arrivalSystem.CalculateNextArrival(State.Reputation);
        }

        // Process staff AI
        ProcessStaffActions();

        // Process customer behavior
        ProcessCustomers();

        // Process fatigue for working staff
        ProcessStaffFatigue();
    }

    /// <summary>
    /// Processes a player action and returns the result
    /// </summary>
    public ActionResult ProcessAction(InnAction action)
    {
        ArgumentNullException.ThrowIfNull(action);

        return action.Type switch
        {
            InnActionTypes.UpgradeKitchen => ProcessUpgradeKitchen(),
            InnActionTypes.UpgradeBar => ProcessUpgradeBar(),
            InnActionTypes.UpgradeRooms => ProcessUpgradeRooms(),
            InnActionTypes.AddGuestRoom => ProcessAddGuestRoom(),
            InnActionTypes.AddStaffBed => ProcessAddStaffBed(),
            InnActionTypes.Advertise => ProcessAdvertise(),
            _ => ActionResult.Fail($"Unknown action: {action.Type}")
        };
    }

    /// <summary>
    /// Adds gold directly to the inn
    /// </summary>
    public InnGameLoop AddGold(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);
        State = State.AddGold(amount);
        return this;
    }

    /// <summary>
    /// Sets the reputation of the inn
    /// </summary>
    public InnGameLoop SetReputation(int reputation)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(reputation);
        State = new InnState(State.Layout, State.Facilities, State.Customers, State.Staff, State.Gold, reputation, State.InnLevel);
        return this;
    }

    private void ProcessStaffActions()
    {
        var newStaff = new List<StaffMember>();

        for (int i = 0; i < State.Staff.Count; i++)
        {
            var staff = State.Staff[i];
            var updated = staff;

            // Process movement first
            if (updated.Position?.IsTraveling == true)
            {
                var graph = GetGraphForStaff(updated);
                if (graph != null)
                {
                    var newPosition = MovementProcessor.ProcessMovement(
                        updated.Position,
                        graph,
                        updated.Efficiency,
                        deltaTime: 1.0
                    );
                    updated = updated.WithPosition(newPosition);
                }
            }

            // Then decide on actions
            var action = StaffAI.DecideAction(updated, State);
            if (action != null)
            {
                updated = ExecuteStaffTask(updated, action, i);
            }

            newStaff.Add(updated);
        }

        State = State with { Staff = newStaff };
    }

    private void ProcessCustomers()
    {
        var newCustomers = new List<Customer>();

        for (int i = 0; i < State.Customers.Count; i++)
        {
            var customer = State.Customers[i];
            var updated = customer;

            // Process movement
            if (updated.Position?.IsTraveling == true)
            {
                var graph = GetGraphForCustomer(updated);
                if (graph != null)
                {
                    var newPosition = MovementProcessor.ProcessMovement(
                        updated.Position,
                        graph,
                        speed: 1.0,
                        deltaTime: 1.0
                    );
                    updated = updated.WithPosition(newPosition);
                }
            }

            // State machine transitions
            updated = updated.State switch
            {
                CustomerState.Eating when !updated.IsEatingComplete => updated.AdvanceEatingProgress(0.1),
                CustomerState.Eating when updated.IsEatingComplete => updated.WithState(CustomerState.Leaving),
                _ => updated
            };

            newCustomers.Add(updated);
        }

        State = State with { Customers = newCustomers };
    }

    private void ProcessStaffFatigue()
    {
        var newStaff = new List<StaffMember>();

        foreach (var staff in State.Staff)
        {
            var updated = staff;

            // Increase fatigue while working
            if (!updated.IsIdle && updated.CurrentTask?.Type != StaffTaskType.Sleep)
            {
                updated = updated.IncreaseFatigue(0.01);
            }

            // Decrease fatigue while sleeping
            if (updated.CurrentTask?.Type == StaffTaskType.Sleep && updated.DesignatedBed != null)
            {
                updated = updated.DecreaseFatigue(0.1);
            }

            newStaff.Add(updated);
        }

        State = State with { Staff = newStaff };
    }

    private StaffMember ExecuteStaffTask(StaffMember staff, StaffTask task, int staffIndex)
    {
        // Execute the task based on type
        return task.Type switch
        {
            StaffTaskType.SeatGuest => ExecuteSeatTask(staff, task),
            StaffTaskType.Serve => ExecuteServeTask(staff, task),
            StaffTaskType.Cook => ExecuteCookTask(staff),
            StaffTaskType.TendBar => ExecuteTendBarTask(staff),
            StaffTaskType.Clean => ExecuteCleanTask(staff),
            StaffTaskType.Sleep => ExecuteSleepTask(staff, task, staffIndex),
            _ => staff.WithTask(task)
        };
    }

    private StaffMember ExecuteSeatTask(StaffMember staff, StaffTask task)
    {
        // Find a waiting customer and seat them
        var customerIndex = FindCustomerIndex(c => c.State == CustomerState.Waiting && c.Name == task.TargetId);
        if (customerIndex >= 0)
        {
            var customer = State.Customers[customerIndex].WithState(CustomerState.Seated);
            UpdateCustomerInState(customerIndex, customer);
        }

        return staff.ClearTask();
    }

    private StaffMember ExecuteServeTask(StaffMember staff, StaffTask task)
    {
        // Find customer and serve them
        var customerIndex = FindCustomerIndex(c => c.Name == task.TargetId);
        if (customerIndex >= 0)
        {
            var customer = State.Customers[customerIndex];
            if (customer.Order == null)
            {
                // Take order
                customer = customer.WithOrder(new CustomerOrder("Roast Chicken", 15));
                UpdateCustomerInState(customerIndex, customer);
            }
            else if (customer.State == CustomerState.Seated)
            {
                // Deliver food
                customer = customer.WithState(CustomerState.Eating);
                UpdateCustomerInState(customerIndex, customer);
            }
        }

        return staff.ClearTask();
    }

    private int FindCustomerIndex(Func<Customer, bool> predicate)
    {
        for (int i = 0; i < State.Customers.Count; i++)
        {
            if (predicate(State.Customers[i]))
            {
                return i;
            }
        }
        return -1;
    }

    private StaffMember ExecuteCookTask(StaffMember staff)
    {
        // Cooking would be handled by a separate kitchen production system
        // For now, just clear the task after some time
        return staff.ClearTask();
    }

    private StaffMember ExecuteTendBarTask(StaffMember staff)
    {
        // Bar tending logic
        return staff.ClearTask();
    }

    private StaffMember ExecuteCleanTask(StaffMember staff)
    {
        // Cleaning logic
        return staff.ClearTask();
    }

    private StaffMember ExecuteSleepTask(StaffMember staff, StaffTask task, int staffIndex)
    {
        // Move to designated bed if not already there
        var bedNodeId = $"staff_bed_{staffIndex + 1}";
        if (staff.Position?.CurrentNode != bedNodeId)
        {
            var graph = GetGraphForStaff(staff);
            if (graph != null && graph.GetNode(bedNodeId) != null)
            {
                var newPosition = staff.Position?.StartTravel(bedNodeId) ?? new EntityPosition(bedNodeId);
                staff = staff.WithPosition(newPosition);
            }
        }

        return staff.WithTask(task);
    }

    private ActionResult ProcessUpgradeKitchen()
    {
        var facility = State.GetFacility("kitchen");
        if (facility == null)
        {
            return ActionResult.Fail("Kitchen not found");
        }

        if (!State.CanAfford(facility.UpgradeCost))
        {
            return ActionResult.Fail($"Not enough gold to upgrade kitchen. Need {facility.UpgradeCost}, have {State.Gold}");
        }

        State = State.RemoveGold(facility.UpgradeCost).UpgradeFacility("kitchen");
        return ActionResult.Ok("Kitchen upgraded");
    }

    private ActionResult ProcessUpgradeBar()
    {
        var facility = State.GetFacility("bar");
        if (facility == null)
        {
            return ActionResult.Fail("Bar not found");
        }

        if (!State.CanAfford(facility.UpgradeCost))
        {
            return ActionResult.Fail($"Not enough gold to upgrade bar. Need {facility.UpgradeCost}, have {State.Gold}");
        }

        State = State.RemoveGold(facility.UpgradeCost).UpgradeFacility("bar");
        return ActionResult.Ok("Bar upgraded");
    }

    private ActionResult ProcessUpgradeRooms()
    {
        // Upgrade all guest rooms
        var upgraded = false;
        foreach (var kvp in State.Facilities)
        {
            if (kvp.Value.Type == "GuestRoom")
            {
                if (State.CanAfford(kvp.Value.UpgradeCost))
                {
                    State = State.RemoveGold(kvp.Value.UpgradeCost).UpgradeFacility(kvp.Key);
                    upgraded = true;
                }
            }
        }

        return upgraded ? ActionResult.Ok("Guest rooms upgraded") : ActionResult.Fail("Not enough gold to upgrade any rooms");
    }

    private ActionResult ProcessAddGuestRoom()
    {
        const int cost = 200;
        if (!State.CanAfford(cost))
        {
            return ActionResult.Fail($"Not enough gold. Need {cost}, have {State.Gold}");
        }

        // Add a new guest room facility
        var newRoomId = $"guest_bed_{State.GetAvailableGuestRooms() + 1}";
        var newFacility = new InnFacility("GuestRoom", 1, 1, 0, 50);
        var newFacilities = State.Facilities.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        newFacilities[newRoomId] = newFacility;

        State = State.RemoveGold(cost) with { Facilities = newFacilities };
        return ActionResult.Ok($"Guest room added: {newRoomId}");
    }

    private ActionResult ProcessAddStaffBed()
    {
        const int cost = 100;
        if (!State.CanAfford(cost))
        {
            return ActionResult.Fail($"Not enough gold. Need {cost}, have {State.Gold}");
        }

        // Add a new staff bed facility
        var newBedId = $"staff_bed_{State.GetAvailableStaffBeds() + 1}";
        var newFacility = new InnFacility("StaffBed", 1, 1, 0, 30);
        var newFacilities = State.Facilities.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        newFacilities[newBedId] = newFacility;

        State = State.RemoveGold(cost) with { Facilities = newFacilities };
        return ActionResult.Ok($"Staff bed added: {newBedId}");
    }

    private ActionResult ProcessAdvertise()
    {
        const int cost = 50;
        if (!State.CanAfford(cost))
        {
            return ActionResult.Fail($"Not enough gold. Need {cost}, have {State.Gold}");
        }

        State = State.RemoveGold(cost).AddReputation(10);
        _arrivalSystem.CalculateNextArrival(State.Reputation);
        return ActionResult.Ok("Advertising complete. Reputation increased by 10");
    }

    private void BuildAreaGraphs()
    {
        foreach (var area in State.Layout.Areas)
        {
            var graph = SceneGraph.GenerateFromTileGrid(area.Grid, area.Id);
            _areaGraphs[area.Id] = graph;
        }
    }

    private SceneGraph? GetGraphForStaff(StaffMember staff)
    {
        if (staff.Position == null)
        {
            return _areaGraphs.Values.FirstOrDefault();
        }

        return _areaGraphs.GetValueOrDefault(staff.Position.CurrentNode);
    }

    private SceneGraph? GetGraphForCustomer(Customer customer)
    {
        if (customer.Position == null)
        {
            return _areaGraphs.Values.FirstOrDefault();
        }

        return _areaGraphs.GetValueOrDefault(customer.Position.CurrentNode);
    }

    private void UpdateCustomerInState(int customerIndex, Customer customer)
    {
        if (customerIndex >= 0 && customerIndex < State.Customers.Count)
        {
            var newCustomers = State.Customers.ToList();
            newCustomers[customerIndex] = customer;
            State = State with { Customers = newCustomers };
        }
    }
}
