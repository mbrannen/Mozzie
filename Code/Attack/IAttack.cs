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
    
    
    //ANIMATION SPECIFIC PROPERTIES

    #region Animation Properties
    public bool CanBeRotated { get; }
    public bool CanMove { get; }
    public bool IsRootedToPlayer { get; }
    

    #endregion
    

}