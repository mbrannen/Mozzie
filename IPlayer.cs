namespace Mozzie;

public interface IPlayer
{
    public string Description { get; set; }
    public int Health { get; set; }
    public int Experience { get; set; }
}