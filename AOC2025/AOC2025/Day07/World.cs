using AOC2025.Common;

namespace AOC2025.Day07;

public class World
{
    public World()
    {
        Id = Guid.NewGuid().ToString();
    }
    public Point2D LastPoint { get; set; }
    public string Id { get; set; }

    public void Add(Point2D point)
    {
        LastPoint = point;
    }
}