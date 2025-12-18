using AOC2025.Common;

namespace AOC2025.Day08;

public class Circuit
{
    public Circuit(IEnumerable<JBox> points)
    {
        Id = Guid.NewGuid().ToString();
        Boxes = points.ToList();
    }
    public List<JBox> Boxes { get; set; }
    public string Id { get; set; }
    public string ClosestCircuitId { get; set; }
    public double DistanceToClosest { get; set; }
    
    public double DistanceTo(Circuit other)
    {
        return Boxes.SelectMany(x => other.Boxes.Select(y => y.Point.DistanceTo(x.Point))).Min();
    }
}