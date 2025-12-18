using AOC2025.Common;

namespace AOC2025.Day08;

public class JBox
{
    public JBox(string input)
    {
        PointAsString = input;
        Point = new Point3D(input);
    }
    public Point3D Point { get; set; }
    public string PointAsString { get; set; }
}