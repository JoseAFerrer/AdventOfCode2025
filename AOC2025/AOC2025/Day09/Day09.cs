using AOC2025.Common;

namespace AOC2025.Day09;

public static class Day09
{
    public static void Solve()
    {
        var input = FileHelpers.ReadInputLines(9, FileHelpers.Input.Sample);
        var points = input.Select(x =>
        {
            var xy = x.Split(',');
            return new Point2D(int.Parse(xy[0]), int.Parse(xy[1]));
        }).ToArray();

        var biggestSquare = new Square(new Point2D(0, 0), new Point2D(1, 1), 1);
        foreach (var first in points)
        {
            foreach (var second in points.Except([first]))
            {
                var area = GetArea(first, second);
                if (area > biggestSquare.Area)
                    biggestSquare = new Square(first, second, area);
            }
        }

        Console.WriteLine($"Biggest square has area {biggestSquare.Area}");
        // 2147419104 too low
    }

    private static int GetArea(Point2D first, Point2D second)
    {
        var xLength = Math.Abs(first.X -  second.X)+1;
        var yLength = Math.Abs(first.Y -  second.Y)+1;
        var area = xLength*yLength;
        return area;
    }
}