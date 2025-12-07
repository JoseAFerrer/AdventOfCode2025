using AOC2025.Common;

namespace AOC2025.Day04;

public class Day04
{
    public static void Solve()
    {
        var input = Enumerable.ToArray(Enumerable.Reverse(FileHelpers.ReadInputLines(4, FileHelpers.Input.Real)));
        var floor = CreateFloorWithPaperRolls(input);

        var accessibleCounter = 0;
        foreach (var roll in floor.PaperRolls)
        {
            var neighbors = roll.GetAllNeighbors();
            var howManyNeighborsAreRollsToo = neighbors.Count(x => floor.PaperRolls.Contains(x));
            var rollIsAccessible = howManyNeighborsAreRollsToo  < 4;
            if (rollIsAccessible)
            {
                accessibleCounter++;
            };
        }
        Console.WriteLine(accessibleCounter);
    }

    private static Floor CreateFloorWithPaperRolls(string[] input)
    {
        var floor = new Floor();
        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            for (int j = 0; j < line.Length; j++)
            {
                if (line[j] == '@') floor.PaperRolls.Add(new Point2D(i, j));
            }
        }

        return floor;
    }
}