using RealmsOfIdle.Core.Infrastructure;

namespace RealmsOfIdle.Core.Scenarios.Inn;

/// <summary>
/// Handles customer arrival logic based on reputation and capacity
/// </summary>
public sealed class CustomerArrivalSystem
{
    private static readonly string[] s_customerNames =
    [
        "Aldric", "Berta", "Cedric", "Dalia", "Edric", "Fiona", "Gareth", "Helena",
        "Ivar", "Jasmine", "Kael", "Lira", "Magnus", "Nora", "Oswin", "Petra",
        "Quinn", "Rina", "Sven", "Talia", "Ulric", "Vera", "Wyn", "Xara"
    ];

    private readonly ArrivalConfig _config;
    private readonly DeterministicRng _rng;

    /// <summary>
    /// Gets the number of ticks until the next customer arrival
    /// </summary>
    public int TicksUntilNextArrival { get; private set; }

    /// <summary>
    /// Gets whether a customer should arrive now
    /// </summary>
    public bool ShouldArrive => TicksUntilNextArrival <= 0;

    /// <summary>
    /// Initializes a new instance of CustomerArrivalSystem
    /// </summary>
    public CustomerArrivalSystem(ArrivalConfig config, DeterministicRng rng)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(rng);

        _config = config;
        _rng = rng;
        TicksUntilNextArrival = 0;
    }

    /// <summary>
    /// Processes a tick and decreases the arrival timer
    /// </summary>
    public void ProcessTick()
    {
        if (TicksUntilNextArrival > 0)
        {
            TicksUntilNextArrival--;
        }
    }

    /// <summary>
    /// Calculates the next arrival time based on reputation
    /// </summary>
    public void CalculateNextArrival(int reputation)
    {
        // Higher reputation = faster arrivals
        // Formula: baseInterval * (1 - (reputation / maxReputation) * 0.8)
        var reputationFactor = (double)reputation / (_config.MaxReputation - _config.MinReputation);
        var intervalMultiplier = 1.0 - (reputationFactor * 0.8);

        TicksUntilNextArrival = (int)(_config.BaseInterval * intervalMultiplier);
        TicksUntilNextArrival = Math.Max(10, TicksUntilNextArrival); // Minimum 10 ticks
    }

    /// <summary>
    /// Checks if a customer can arrive based on capacity
    /// </summary>
    public bool CanArrive(int currentCustomerCount) => currentCustomerCount <= _config.MaxCapacity;

    /// <summary>
    /// Generates a new customer with a random name
    /// </summary>
    public Customer GenerateCustomer()
    {
        var nameIndex = _rng.Next(s_customerNames.Length);
        var name = s_customerNames[nameIndex];
        return new Customer(name);
    }
}
