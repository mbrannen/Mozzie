namespace Mozzie.Code.Attack;

public interface IAttack
{
    public string Name { get; }
    public string Description { get;}
    public float Timer { get; }
    public int Damage { get; }

    public int Count { get; }
    
}