using Godot;

namespace Mozzie.Code.Talents;

public partial class TalentModel
{
    public float Timer { get; set; }
    public int Damage { get; set; }
    public int Count { get; set; }
    public float CriticalChance { get; set; }
}