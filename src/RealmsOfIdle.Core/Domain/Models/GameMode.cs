using Ardalis.SmartEnum;

namespace RealmsOfIdle.Core.Domain.Models;

public abstract class GameMode : SmartEnum<GameMode>
{
    public static readonly GameMode Offline = new OfflineType();
    public static readonly GameMode Online = new OnlineType();

    protected GameMode(string name, int value) : base(name, value) { }

    private class OfflineType : GameMode
    {
        public OfflineType() : base("Offline", 0) { }
    }

    private class OnlineType : GameMode
    {
        public OnlineType() : base("Online", 1) { }
    }
}