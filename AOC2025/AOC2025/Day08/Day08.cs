using AOC2025.Common;

namespace AOC2025.Day08;

public class Day08
{
    public static void Solve()
    {
        var input = FileHelpers.ReadInputLines(8, FileHelpers.Input.Sample);
        var circuits = input
            .Select(line => new JBox(line))
            .Select(x => new Circuit([x]));

        foreach (var circuit in circuits)
        {
        }
        
    }
}