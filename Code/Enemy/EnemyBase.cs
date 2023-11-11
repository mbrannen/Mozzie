using Godot;

namespace Mozzie.Code.Enemy;

public abstract class EnemyBase : IEnemy
{
    public virtual string Name => "Not Implemented";
    public virtual string Description => "Not Implemented";
    public abstract EnemyType Type { get; }
    public EnemyState State { get; private set; }
    public bool IsDead { get; set; } = false;
    public virtual int BaseHealth => 0;
    public int Health { get; set; }
    public virtual int BaseDamage => 0;
    public int Damage { get; set; }
    public virtual float BaseSpeed => 0f;
    public virtual float Speed { get; set; }
    
    public Node2D Player { get; set; }
    public Vector2 Position { get; set; }
    
    private Vector2 RecoilVector { get; set; }

    private const float SPEED_DECAY_RATE = .75f;

    public EnemyBase()
    {
        Health = BaseHealth;
        Speed = BaseSpeed;
        Damage = BaseDamage;
        State = EnemyState.Pursuit;
    }



    public void Navigate(float delta)
    {
        if (State == EnemyState.Pursuit)
        {
            Speed = BaseSpeed;
            var moveTowardsVector = (Player.Position - Position).Normalized() * Speed * delta; //go forwards towards the player
            Position += new Vector2(moveTowardsVector.X, moveTowardsVector.Y);
        }

        if (State == EnemyState.Recoil)
        {
            Speed *= SPEED_DECAY_RATE;
            RecoilVector *= (float)((Speed*4) * delta); //go backwards from the player at a a higher speed
            Position += new Vector2(RecoilVector.X, RecoilVector.Y);
        }
    }
    
    public void CheckHealth()
    {
        IsDead = Health <= 0;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        CheckHealth();
    }

    public void ChangePursuitState()
    {
        Speed = BaseSpeed;
        switch (State)
        {
            case EnemyState.Pursuit:
                RecoilVector = -(Player.Position - Position).Normalized();
                State = EnemyState.Recoil;
                break;
            case EnemyState.Recoil:
                State = EnemyState.Pursuit;
                break;
            default:
                State = EnemyState.Pursuit;
                break;
        }
    }
}

public enum EnemyType
{
    Null,
    ZombieKid
}

public enum EnemyState
{
    Pursuit,
    Recoil
}