using AOC2025.Common;

namespace AOC2025.Day08;

public class Day08
{
    public static void Solve()
    {
        var input = FileHelpers.ReadInputLines(8, FileHelpers.Input.Sample);
        var circuits = input
            .Select(line => new JBox(line))
            .Select(x => new Circuit([x]))
            .ToArray();
        
        CalculateDistancesToClosest(circuits);

        var close = circuits.Where(x => x.DistanceToClosest < 320);
    }

    private static void CalculateDistancesToClosest(Circuit[] circuits)
    {
        foreach (var circuit in circuits)
        {
            var distancesAndIds = circuits
                .Where(x => x.Id != circuit.Id)
                .Select(x => new { x.Id, distance = x.DistanceTo(circuit)})
                .OrderBy(x => x.distance)
                .ToArray();
            
            circuit.ClosestCircuitId = distancesAndIds.First().Id;
            circuit.DistanceToClosest = distancesAndIds.First().distance;
        }
    }
}