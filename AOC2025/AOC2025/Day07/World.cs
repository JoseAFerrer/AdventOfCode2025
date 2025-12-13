using AOC2025.Common;

namespace AOC2025.Day07;

public class World
{
    public World()
    {
        Id = Guid.NewGuid().ToString();
    }
    public World(World previousWorld)
    {
        Id = Guid.NewGuid().ToString();
        Points = previousWorld.Points;
    }
    public HashSet<Point2D> Points { get; set; } = [];
    public Point2D LastPoint { get; set; }
    public string Id { get; set; }

    public void Add(Point2D point)
    {
        Points.Add(point);
        LastPoint = point;
    }
}