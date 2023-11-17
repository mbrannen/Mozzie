namespace Mozzie.Code.Player;

public interface IStat<out T>
{
    public StatType StatType { get; }
    public T Value { get; }
}