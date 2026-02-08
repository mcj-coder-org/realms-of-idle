using RealmsOfIdle.Core.Scenarios.Inn;
using RealmsOfIdle.Core.Engine.Spatial;
using RealmsOfIdle.Client.UI.Components.HUD;

namespace RealmsOfIdle.Client.UI.Components.Game;

/// <summary>
/// View model for the Inn game, connecting game state to UI components
/// </summary>
public sealed class InnGameViewModel
{
    private InnState _state;

    /// <summary>
    /// Gets the current game state
    /// </summary>
    public InnState State => _state;

    /// <summary>
    /// Gets the current gold amount
    /// </summary>
    public int Gold => _state.Gold;

    /// <summary>
    /// Gets the current reputation
    /// </summary>
    public int Reputation => _state.Reputation;

    /// <summary>
    /// Gets the inn level
    /// </summary>
    public int InnLevel => _state.InnLevel;

    /// <summary>
    /// Gets the current customers
    /// </summary>
    public IReadOnlyList<Customer> Customers => _state.Customers;

    /// <summary>
    /// Gets the current staff members
    /// </summary>
    public IReadOnlyList<StaffMember> Staff => _state.Staff;

    /// <summary>
    /// Gets the facilities
    /// </summary>
    public IReadOnlyDictionary<string, InnFacility> Facilities => _state.Facilities;

    /// <summary>
    /// Gets the world layout
    /// </summary>
    public WorldLayout Layout => _state.Layout;

    /// <summary>
    /// Initializes a new instance of InnGameViewModel
    /// </summary>
    public InnGameViewModel(InnState initialState)
    {
        ArgumentNullException.ThrowIfNull(initialState);
        _state = initialState;
    }

    /// <summary>
    /// Updates the view model with a new game state
    /// </summary>
    public void UpdateState(InnState newState)
    {
        ArgumentNullException.ThrowIfNull(newState);
        _state = newState;
    }

    /// <summary>
    /// Gets customer sprites for rendering on the canvas
    /// </summary>
    public IReadOnlyList<EntitySprite> GetCustomerSprites()
    {
        var sprites = new List<EntitySprite>();
        foreach (var customer in _state.Customers)
        {
            if (customer.Position != null)
            {
                sprites.Add(new EntitySprite
                {
                    Id = customer.Name,
                    Type = EntityType.Customer,
                    SpriteId = GetCustomerSpriteId(customer),
                    GridPosition = customer.Position.CurrentNode,
                    TravelProgress = customer.Position.TravelProgress,
                    TargetPosition = customer.Position.TargetNode,
                    State = GetCustomerStateString(customer)
                });
            }
        }
        return sprites;
    }

    /// <summary>
    /// Gets staff sprites for rendering on the canvas
    /// </summary>
    public IReadOnlyList<EntitySprite> GetStaffSprites()
    {
        var sprites = new List<EntitySprite>();
        foreach (var staff in _state.Staff)
        {
            if (staff.Position != null)
            {
                sprites.Add(new EntitySprite
                {
                    Id = staff.Name,
                    Type = EntityType.Staff,
                    SpriteId = GetStaffSpriteId(staff),
                    GridPosition = staff.Position.CurrentNode,
                    TravelProgress = staff.Position.TravelProgress,
                    TargetPosition = staff.Position.TargetNode,
                    State = GetStaffStateString(staff)
                });
            }
        }
        return sprites;
    }

    /// <summary>
    /// Gets facility sprites for rendering on the canvas
    /// </summary>
    public IReadOnlyList<FacilitySprite> GetFacilitySprites()
    {
        var sprites = new List<FacilitySprite>();
        foreach (var kvp in _state.Facilities)
        {
            var facility = kvp.Value;
            sprites.Add(new FacilitySprite
            {
                Id = kvp.Key,
                Type = facility.Type,
                Level = facility.Level,
                ProductionRate = facility.ProductionRate,
                UpgradeCost = facility.UpgradeCost
            });
        }
        return sprites;
    }

    /// <summary>
    /// Gets info panel content for a customer
    /// </summary>
    public InfoPanelContent GetCustomerInfo(Customer customer)
    {
        return new InfoPanelContent
        {
            Title = customer.Name,
            Subtitle = "Customer",
            Icon = "👤",
            Stats = new Dictionary<string, string>
            {
                { "State", customer.State.ToString() },
                { "Satisfaction", customer.Satisfaction.ToString("F1") },
                { "Payment", customer.PaymentAmount.ToString() }
            }
        };
    }

    /// <summary>
    /// Gets info panel content for a staff member
    /// </summary>
    public InfoPanelContent GetStaffInfo(StaffMember staff)
    {
        return new InfoPanelContent
        {
            Title = staff.Name,
            Subtitle = staff.Role,
            Icon = GetStaffIcon(staff.Role),
            Stats = new Dictionary<string, string>
            {
                { "Efficiency", staff.Efficiency.ToString("F2") },
                { "Fatigue", $"{staff.Fatigue:P0}" },
                { "Task", staff.CurrentTask?.Type.ToString() ?? "Idle" }
            }
        };
    }

    private string GetCustomerSpriteId(Customer customer)
    {
        // Return sprite ID based on customer state
        return customer.State switch
        {
            CustomerState.Arriving => "customer_walking",
            CustomerState.Waiting => "customer_waiting",
            CustomerState.Seated => "customer_seated",
            CustomerState.Eating => "customer_eating",
            CustomerState.Leaving => "customer_walking",
            _ => "customer_idle"
        };
    }

    private string GetStaffSpriteId(StaffMember staff)
    {
        // Return sprite ID based on staff role
        return staff.Role switch
        {
            "Cook" => "staff_cook",
            "Waitress" or "Waiter" => "staff_waiter",
            "Bartender" => "staff_bartender",
            _ => "staff_generic"
        };
    }

    private string GetCustomerStateString(Customer customer)
    {
        return customer.State.ToString().ToUpperInvariant();
    }

    private string GetStaffStateString(StaffMember staff)
    {
        if (staff.CurrentTask is null || staff.CurrentTask.Type == StaffTaskType.None)
        {
            return "IDLE";
        }

        return staff.CurrentTask.Type.ToString().ToUpperInvariant();
    }

    private string GetStaffIcon(string role)
    {
        return role switch
        {
            "Cook" => "👨‍🍳",
            "Waitress" => "🧑‍🍺",
            "Waiter" => "🧑‍🍺",
            "Bartender" => "🍷",
            _ => "👤"
        };
    }

    /// <summary>
    /// Gets player stats for HUD display
    /// </summary>
    public PlayerStats PlayerStats => new()
    {
        Gold = _state.Gold,
        PlayerLevel = 1,
        InnLevel = _state.InnLevel,
        Health = 100,
        MaxHealth = 100
    };

    /// <summary>
    /// Gets action slots for the inn scenario
    /// </summary>
    public IReadOnlyList<QuickActionSlot> GetInnActionSlots()
    {
        return new List<QuickActionSlot>
        {
            new() { Id = "upgrade_kitchen", Label = "Upgrade Kitchen", Icon = "🍳", KeyHint = "1", IsActive = CanUpgradeKitchen() },
            new() { Id = "upgrade_bar", Label = "Upgrade Bar", Icon = "🍺", KeyHint = "2", IsActive = CanUpgradeBar() },
            new() { Id = "add_guest_room", Label = "Add Room", Icon = "🛏️", KeyHint = "3", IsActive = CanAddGuestRoom() },
            new() { Id = "advertise", Label = "Advertise", Icon = "📢", KeyHint = "4", IsActive = CanAdvertise() }
        };
    }

    private bool CanUpgradeKitchen()
    {
        var facility = _state.GetFacility("kitchen");
        return facility != null && _state.CanAfford(facility.UpgradeCost);
    }

    private bool CanUpgradeBar()
    {
        var facility = _state.GetFacility("bar");
        return facility != null && _state.CanAfford(facility.UpgradeCost);
    }

    private bool CanAddGuestRoom()
    {
        return _state.CanAfford(200);
    }

    private bool CanAdvertise()
    {
        return _state.CanAfford(50);
    }
}

/// <summary>
/// Entity sprite for canvas rendering
/// </summary>
public record EntitySprite
{
    public required string Id { get; init; }
    public required EntityType Type { get; init; }
    public required string SpriteId { get; init; }
    public required string GridPosition { get; init; }
    public double TravelProgress { get; init; }
    public string? TargetPosition { get; init; }
    public required string State { get; init; }
}

/// <summary>
/// Facility sprite for canvas rendering
/// </summary>
public record FacilitySprite
{
    public required string Id { get; init; }
    public required string Type { get; init; }
    public int Level { get; init; }
    public double ProductionRate { get; init; }
    public int UpgradeCost { get; init; }
}

/// <summary>
/// Entity type enumeration
/// </summary>
public enum EntityType
{
    Customer,
    Staff
}
