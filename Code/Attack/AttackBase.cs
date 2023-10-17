using System;
using System.Runtime.CompilerServices;

namespace Mozzie.Code.Attack;

public abstract class AttackBase : IAttack
{
    public virtual string Name => "Not Implemented";
    public virtual string Description => "Not Implemented";
    public virtual float Timer => 1f;
    public virtual int Damage => 0;
    public virtual int Count => 0;
    
}

public enum AttackNames
{
    Cough,
    Crows,
    Kick
}