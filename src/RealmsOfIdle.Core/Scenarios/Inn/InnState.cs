using RealmsOfIdle.Core.Engine.Spatial;

namespace RealmsOfIdle.Core.Scenarios.Inn;

/// <summary>
/// Represents the complete state of the Inn scenario
/// </summary>
public sealed record class InnState(
    WorldLayout Layout,
    IReadOnlyDictionary<string, InnFacility> Facilities,
    IReadOnlyList<Customer> Customers = null!,
    IReadOnlyList<StaffMember> Staff = null!,
    int Gold = 0,
    int Reputation = 0,
    int InnLevel = 1)
{
    /// <summary>
    /// Initializes a new instance of InnState with empty collections
    /// </summary>
    public InnState(WorldLayout layout, IReadOnlyDictionary<string, InnFacility> facilities)
        : this(layout, facilities, Array.Empty<Customer>(), Array.Empty<StaffMember>())
    {
    }

    /// <summary>
    /// Adds a customer to the inn
    /// </summary>
    public InnState AddCustomer(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);
        var newCustomers = Customers.Append(customer).ToList();
        return this with { Customers = newCustomers };
    }

    /// <summary>
    /// Removes a customer from the inn
    /// </summary>
    public InnState RemoveCustomer(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);
        var newCustomers = Customers.Where(c => c != customer).ToList();
        return this with { Customers = newCustomers };
    }

    /// <summary>
    /// Adds a staff member to the inn
    /// </summary>
    public InnState AddStaff(StaffMember staff)
    {
        ArgumentNullException.ThrowIfNull(staff);
        var newStaff = Staff.Append(staff).ToList();
        return this with { Staff = newStaff };
    }

    /// <summary>
    /// Removes a staff member from the inn
    /// </summary>
    public InnState RemoveStaff(StaffMember staff)
    {
        ArgumentNullException.ThrowIfNull(staff);
        var newStaff = Staff.Where(s => s != staff).ToList();
        return this with { Staff = newStaff };
    }

    /// <summary>
    /// Creates a new inn state with gold added
    /// </summary>
    public InnState AddGold(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);
        return this with { Gold = Gold + amount };
    }

    /// <summary>
    /// Creates a new inn state with gold removed (clamped at 0)
    /// </summary>
    public InnState RemoveGold(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);
        var newGold = Math.Max(0, Gold - amount);
        return this with { Gold = newGold };
    }

    /// <summary>
    /// Checks if the inn can afford a purchase
    /// </summary>
    public bool CanAfford(int cost) => Gold >= cost;

    /// <summary>
    /// Creates a new inn state with reputation added
    /// </summary>
    public InnState AddReputation(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);
        return this with { Reputation = Reputation + amount };
    }

    /// <summary>
    /// Creates a new inn state with the inn level increased
    /// </summary>
    public InnState LevelUp()
    {
        return this with { InnLevel = InnLevel + 1 };
    }

    /// <summary>
    /// Gets a facility by its ID
    /// </summary>
    public InnFacility? GetFacility(string facilityId)
    {
        if (Facilities.TryGetValue(facilityId, out var facility))
        {
            return facility;
        }

        // Also try by type for backward compatibility
        foreach (var fac in Facilities.Values)
        {
            if (fac.Type.Equals(facilityId, StringComparison.OrdinalIgnoreCase))
            {
                return fac;
            }
        }

        return null;
    }

    /// <summary>
    /// Creates a new inn state with a facility upgraded
    /// </summary>
    public InnState UpgradeFacility(string facilityId)
    {
        var facility = GetFacility(facilityId);
        if (facility == null)
        {
            throw new ArgumentException($"Facility '{facilityId}' not found.", nameof(facilityId));
        }

        var upgradedFacility = facility.Upgrade();
        var newFacilities = Facilities.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value == facility ? upgradedFacility : kvp.Value
        );

        return this with { Facilities = newFacilities };
    }

    /// <summary>
    /// Gets the count of available guest room beds
    /// </summary>
    public int GetAvailableGuestRooms()
    {
        return Facilities.Values
            .Count(f => f.Type == "GuestRoom");
    }

    /// <summary>
    /// Gets the count of available staff beds
    /// </summary>
    public int GetAvailableStaffBeds()
    {
        return Facilities.Values
            .Count(f => f.Type == "StaffBed");
    }
}
