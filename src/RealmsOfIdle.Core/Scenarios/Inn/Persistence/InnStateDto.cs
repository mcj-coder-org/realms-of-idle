using RealmsOfIdle.Core.Engine.Spatial;

namespace RealmsOfIdle.Core.Scenarios.Inn.Persistence;

/// <summary>
/// Data transfer object for persisting InnState to LiteDB
/// </summary>
public class InnStateDto
{
    public string PlayerId { get; set; } = string.Empty;
    public int Gold { get; set; }
    public int Reputation { get; set; }
    public int InnLevel { get; set; }
    public List<CustomerDto> Customers { get; set; } = new();
    public List<StaffDto> Staff { get; set; } = new();
    public List<FacilityDto> Facilities { get; set; } = new();
    public List<SceneAreaDto> LayoutAreas { get; set; } = new();
    public int CurrentTick { get; set; }
    public DateTime LastTickTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Converts domain InnState to DTO for persistence
    /// </summary>
    public static InnStateDto FromDomain(InnState state, string playerId, int currentTick = 0)
    {
        return new InnStateDto
        {
            PlayerId = playerId,
            Gold = state.Gold,
            Reputation = state.Reputation,
            InnLevel = state.InnLevel,
            Customers = state.Customers.Select(CustomerDto.FromDomain).ToList(),
            Staff = state.Staff.Select(StaffDto.FromDomain).ToList(),
            Facilities = state.Facilities.Select(kvp => FacilityDto.FromDomain(kvp.Key, kvp.Value)).ToList(),
            LayoutAreas = state.Layout.Areas.Select(SceneAreaDto.FromDomain).ToList(),
            CurrentTick = currentTick,
            LastTickTime = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Converts DTO back to domain InnState
    /// </summary>
    public InnState ToDomain()
    {
        var layout = new WorldLayout();
        foreach (var areaDto in LayoutAreas)
        {
            var area = areaDto.ToDomain();
            layout.AddArea(area);
        }

        // Handle duplicate/empty IDs gracefully by filtering and taking first of each ID
        var facilitiesList = Facilities.ToList(); // Materialize to avoid modification during enumeration
        var facilities = new Dictionary<string, InnFacility>();
        foreach (var dto in facilitiesList)
        {
            if (!string.IsNullOrEmpty(dto.Id) && !facilities.ContainsKey(dto.Id))
            {
                facilities[dto.Id] = dto.ToDomain();
            }
        }

        return new InnState(
            layout,
            facilities,
            Customers.Select(dto => dto.ToDomain()).ToList(),
            Staff.Select(dto => dto.ToDomain()).ToList(),
            Gold,
            Reputation,
            InnLevel
        );
    }
}

/// <summary>
/// DTO for Customer entity
/// </summary>
public class CustomerDto
{
    public string Name { get; set; } = string.Empty;
    public string State { get; set; } = nameof(CustomerState.Waiting);
    public double EatingProgress { get; set; }
    public string? OrderItemName { get; set; }
    public int OrderPrice { get; set; }
    public int Satisfaction { get; set; }
    public int PaymentAmount { get; set; }
    public string? CurrentNode { get; set; }
    public string? TargetNode { get; set; }
    public double TravelProgress { get; set; }

    public static CustomerDto FromDomain(Customer customer)
    {
        return new CustomerDto
        {
            Name = customer.Name,
            State = customer.State.ToString(),
            EatingProgress = customer.EatingProgress,
            OrderItemName = customer.Order?.ItemName,
            OrderPrice = customer.Order?.Price ?? 0,
            Satisfaction = (int)customer.Satisfaction,
            PaymentAmount = customer.PaymentAmount,
            CurrentNode = customer.Position?.CurrentNode,
            TargetNode = customer.Position?.TargetNode,
            TravelProgress = customer.Position?.TravelProgress ?? 0.0
        };
    }

    public Customer ToDomain()
    {
        var customer = new Customer(Name);
        customer = customer.WithState(Enum.Parse<CustomerState>(State));
        customer = customer.AdvanceEatingProgress(EatingProgress);
        if (!string.IsNullOrEmpty(OrderItemName))
        {
            customer = customer.WithOrder(new CustomerOrder(OrderItemName, OrderPrice));
        }
        if (PaymentAmount > 0)
        {
            customer = customer.WithPaymentAmount(PaymentAmount);
        }
        if (Satisfaction > 0)
        {
            customer = customer.IncreaseSatisfaction(Satisfaction);
        }
        if (!string.IsNullOrEmpty(CurrentNode))
        {
            var position = new EntityPosition(CurrentNode);
            if (!string.IsNullOrEmpty(TargetNode))
            {
                position = position.StartTravel(TargetNode);
                position = position.WithTravelProgress(TravelProgress);
            }
            customer = customer.WithPosition(position);
        }
        return customer;
    }
}

/// <summary>
/// DTO for StaffMember entity
/// </summary>
public class StaffDto
{
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public double Efficiency { get; set; } = 1.0;
    public double Fatigue { get; set; }
    public string? CurrentNode { get; set; }
    public string? TargetNode { get; set; }
    public double TravelProgress { get; set; }
    public string? CurrentTaskType { get; set; }
    public string? TaskTargetId { get; set; }

    public static StaffDto FromDomain(StaffMember staff)
    {
        return new StaffDto
        {
            Name = staff.Name,
            Role = staff.Role,
            Efficiency = staff.Efficiency,
            Fatigue = staff.Fatigue,
            CurrentNode = staff.Position?.CurrentNode,
            TargetNode = staff.Position?.TargetNode,
            TravelProgress = staff.Position?.TravelProgress ?? 0.0,
            CurrentTaskType = staff.CurrentTask?.Type.ToString(),
            TaskTargetId = staff.CurrentTask?.TargetId
        };
    }

    public StaffMember ToDomain()
    {
        var staff = new StaffMember(Name, Role);
        if (Efficiency != 1.0)
        {
            staff = staff.WithEfficiency(Efficiency);
        }
        if (Fatigue > 0)
        {
            staff = staff.WithFatigue(Fatigue);
        }

        if (!string.IsNullOrEmpty(CurrentNode))
        {
            var position = new EntityPosition(CurrentNode);
            if (!string.IsNullOrEmpty(TargetNode))
            {
                position = position.StartTravel(TargetNode);
                position = position.WithTravelProgress(TravelProgress);
            }
            staff = staff.WithPosition(position);
        }

        if (!string.IsNullOrEmpty(CurrentTaskType) && Enum.TryParse<StaffTaskType>(CurrentTaskType, out var taskType))
        {
            staff = staff.WithTask(new StaffTask(taskType, TaskTargetId ?? string.Empty));
        }

        return staff;
    }
}

/// <summary>
/// DTO for InnFacility
/// </summary>
public class FacilityDto
{
    public string Id { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Level { get; set; } = 1;
    public int Capacity { get; set; }
    public double ProductionRate { get; set; }
    public int UpgradeCost { get; set; }

    public static FacilityDto FromDomain(string id, InnFacility facility)
    {
        return new FacilityDto
        {
            Id = id,
            Type = facility.Type,
            Level = facility.Level,
            Capacity = facility.Capacity,
            ProductionRate = facility.ProductionRate,
            UpgradeCost = facility.UpgradeCost
        };
    }

    public InnFacility ToDomain()
    {
        return new InnFacility(Type, Level, Capacity, ProductionRate, UpgradeCost);
    }
}

/// <summary>
/// DTO for SceneArea (part of WorldLayout)
/// </summary>
public class SceneAreaDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Width { get; set; }
    public int Height { get; set; }
    public List<TileDto> Tiles { get; set; } = new();

    public static SceneAreaDto FromDomain(SceneArea area)
    {
        return new SceneAreaDto
        {
            Id = area.Id,
            Name = area.Name,
            Width = area.Grid.Width,
            Height = area.Grid.Height,
            Tiles = SerializeTiles(area.Grid)
        };
    }

    public SceneArea ToDomain()
    {
        // Create the area with the original dimensions
        var area = new SceneArea(Id, Name, Width, Height);

        // Set each non-empty tile
        foreach (var tile in Tiles)
        {
            if (tile.Type == "Door")
            {
                area.AddDoorTile(new GridPosition(tile.X, tile.Y));
            }
            else
            {
                area.Grid.SetTile(tile.X, tile.Y, CreateTile(tile.Type));
            }
        }

        return area;
    }

    private static List<TileDto> SerializeTiles(TileGrid grid)
    {
        var tiles = new List<TileDto>();
        for (var y = 0; y < grid.Height; y++)
        {
            for (var x = 0; x < grid.Width; x++)
            {
                var tile = grid.GetTile(x, y);
                if (!ReferenceEquals(tile, TileType.Empty))
                {
                    tiles.Add(new TileDto { X = x, Y = y, Type = tile.Name });
                }
            }
        }
        return tiles;
    }

    private static TileType CreateTile(string typeName)
    {
        return typeName switch
        {
            "Floor" => TileType.Floor,
            "Wall" => TileType.Wall,
            "Door" => TileType.Door,
            "Furniture" => TileType.Furniture,
            _ => TileType.Empty
        };
    }
}

/// <summary>
/// DTO for Tile
/// </summary>
public class TileDto
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Type { get; set; } = string.Empty;
}
