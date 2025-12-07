using AOC2025.Common;

namespace AOC2025.Day04;

public class Day04
{
    public static void Solve()
    {
        var input = FileHelpers.ReadInputLines(4, FileHelpers.Input.Real).Reverse().ToArray();
        var floor = CreateFloorWithPaperRolls(input);

        var accessibleCounter = 0;
        for (int i = 0; i < 80; i++)
        {
            Console.WriteLine($"Step {i}, counter: {accessibleCounter}. Remaining {floor.PaperRollsAndHasBeenRemoved.Count(v => !v.Value)}");
            accessibleCounter = CalculateAccessibleRollsAndMarkThemAsRemoved(floor, accessibleCounter);
        }
        
        Console.WriteLine(accessibleCounter);
    }

    private static int CalculateAccessibleRollsAndMarkThemAsRemoved(Floor floor, int accessibleCounter)
    {
        var paperRollsNotYetRemoved = floor
            .PaperRollsAndHasBeenRemoved
            .Where(v => !v.Value).ToDictionary();
        
        foreach (var (roll, _) in paperRollsNotYetRemoved)
        {
            var neighbors = roll.GetAllNeighbors();
            var howManyNeighborsAreRollsToo = neighbors
                .Count(x => paperRollsNotYetRemoved.ContainsKey(x) && !paperRollsNotYetRemoved[x]);

            if (howManyNeighborsAreRollsToo >= 4) continue;
            accessibleCounter++;
            floor.PaperRollsAndHasBeenRemoved[roll] = true;
        }

        return accessibleCounter;
    }

    private static Floor CreateFloorWithPaperRolls(string[] input)
    {
        var floor = new Floor();
        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            for (int j = 0; j < line.Length; j++)
            {
                if (line[j] == '@') floor.PaperRollsAndHasBeenRemoved.Add(new Point2D(i, j), false);
            }
        }

        return floor;
    }
}