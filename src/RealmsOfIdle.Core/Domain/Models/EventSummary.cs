namespace RealmsOfIdle.Core.Domain.Models;

/// <summary>
/// Summary of events for analytics purposes
/// </summary>
public class EventSummary
{
    public string PlayerId { get; set; } = string.Empty;
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public int TotalEvents { get; set; }
    public Dictionary<string, int> EventCounts { get; set; } = new();
    public List<string> UniqueEventTypes { get; set; } = new();
}
