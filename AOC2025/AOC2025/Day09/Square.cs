using AOC2025.Common;

namespace AOC2025.Day09;

public class Square
{
    public Square(Point2D firstCorner , Point2D secondCorner, long area)
    {
        FirstCorner = firstCorner;
        SecondCorner = secondCorner;
        Area = area;
    }
    public Point2D FirstCorner { get; set; }
    public Point2D SecondCorner { get; set; }
    public long Area { get; set; }
}