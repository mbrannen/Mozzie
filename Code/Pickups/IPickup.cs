namespace Mozzie.Code.Pickups;

public interface IPickup
{
    public string Name { get; }
    public string Description { get; }
    public int Value { get; }
    public float Chance { get; }
}