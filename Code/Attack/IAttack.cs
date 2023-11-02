namespace Mozzie.Code.Attack;

public interface IAttack
{
    public bool Enabled { get; set; }
    public string Name { get; }
    public string Description { get;}
    public float BaseTimer { get; }
    public float Timer { get; set; }
    public int BaseDamage { get; }
    public int Damage { get; set; }
    public int BaseCount { get; }
    public int Count { get; set; }
    public float BaseCriticalChance { get; }
    public float CriticalChance { get; set; }

}