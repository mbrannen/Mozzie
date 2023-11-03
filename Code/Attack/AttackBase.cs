using System;
using System.Runtime.CompilerServices;
using Godot;
using Mozzie.Code.Talents;

namespace Mozzie.Code.Attack;

public abstract class AttackBase : IAttack
{
    public bool Enabled { get; set; }
    public virtual string Name => "Not Implemented";
    public virtual string Description => "Not Implemented";
    public virtual float BaseTimer => 1f;
    public float Timer { get; set; }
    public virtual int BaseDamage => 0;
    public int Damage { get; set; }
    public virtual int BaseCount => 0;
    public int Count { get; set; }
    public virtual float BaseCriticalChance => 0;
    public float CriticalChance { get; set; }
    public abstract bool CanBeRotated { get; }

    public AttackBase(TalentModel talents)
    {
        Timer = BaseTimer + talents.Timer;
        Damage = BaseDamage + talents.Damage;
        Count = BaseCount + talents.Count;
        CriticalChance = BaseCriticalChance + talents.CriticalChance;

    }
    
    public virtual bool IsCrit()
    {
        return GD.RandRange(0.01, 1) < CriticalChance;
    }

    public virtual int CalculateDamage()
    {
        if (IsCrit())
            return Damage * 2;
        return Damage;
    }
}

public enum AttackNames
{
    Cough,
    Crows,
    Shiv
}