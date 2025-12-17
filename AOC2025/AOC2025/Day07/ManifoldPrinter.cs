using System.Text;
using AOC2025.Common;

namespace AOC2025.Day07;

public static class ManifoldPrinter
{
    public static void Print(this TachyonManifold manifold)
    {
        Console.WriteLine();
        for (int i = manifold.Rows - 1; i >= 0; i--)
        {
            var rowToPrint = new StringBuilder();
            for (int j = 0; j < manifold.Columns; j++)
            {
                var point = manifold.Points[new Point2D(j, i)];
                rowToPrint.Append("_" + point.WorldsWhereParticlePassesThroughHere + "_");
            }

            Console.WriteLine(rowToPrint);
        }
    }
}