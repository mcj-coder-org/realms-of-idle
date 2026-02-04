using Ardalis.SmartEnum;

namespace RealmsOfIdle.Core.Domain.Models;

public abstract class HealthStatus : SmartEnum<HealthStatus>
{
    public static readonly HealthStatus Healthy = new HealthyType();
    public static readonly HealthStatus Unhealthy = new UnhealthyType();
    public static readonly HealthStatus Degraded = new DegradedType();

    protected HealthStatus(string name, int value) : base(name, value) { }

    private class HealthyType : HealthStatus
    {
        public HealthyType() : base("Healthy", 0) { }
    }

    private class UnhealthyType : HealthStatus
    {
        public UnhealthyType() : base("Unhealthy", 1) { }
    }

    private class DegradedType : HealthStatus
    {
        public DegradedType() : base("Degraded", 2) { }
    }
}
