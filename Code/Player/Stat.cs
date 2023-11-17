namespace Mozzie.Code.Player;

public class Stat<T> : IStat<T>
{
    public StatType StatType { get; }
    public T Value { get; set; }
    
    public Stat(StatType statType, T value)
    {
        StatType = statType;
        Value = value;
    }
}