using AOC2025.Common;

namespace AOC2025.Day08;

public class Day08
{
    public static void Solve()
    {
        const FileHelpers.Input inputType = FileHelpers.Input.Sample;
        var circuits = GetCircuits(inputType);
        CalculateDistancesToClosest(circuits);

        for (var i = 0; i < 10; i++)
        {
            var minDistance = circuits.Min(x => x.DistanceToClosest);
            
            var firstCircuit = circuits.First(x => x.DistanceToClosest <= minDistance);
            var secondCircuit = circuits.First(x => x.Id == firstCircuit.ClosestCircuitId);
            
            CombineTwoCircuits(firstCircuit, secondCircuit, circuits);

            circuits.Remove(secondCircuit);
        }
    }

    private static void CombineTwoCircuits(Circuit firstCircuit, Circuit secondCircuit, List<Circuit> circuits)
    {
        Console.WriteLine($"Combining circuits with boxes {firstCircuit.BoxesLocations} and {secondCircuit.BoxesLocations} with distance {firstCircuit.DistanceToClosest}");
        firstCircuit.Boxes.AddRange(secondCircuit.Boxes);
        firstCircuit.DistanceToClosest = circuits.Max(x => x.DistanceToClosest);
        foreach (var iteratingCircuit in circuits)
        {
            if (iteratingCircuit.ClosestCircuitId != secondCircuit.Id) continue;
                
            iteratingCircuit.ClosestCircuitId = firstCircuit.Id;
            if (iteratingCircuit.DistanceToClosest <= firstCircuit.DistanceToClosest)
            {
                firstCircuit.DistanceToClosest = iteratingCircuit.DistanceToClosest;
                firstCircuit.ClosestCircuitId = iteratingCircuit.Id;
            }
        }
    }

    private static List<Circuit> GetCircuits(FileHelpers.Input inputType)
    {
        var input = FileHelpers.ReadInputLines(8, inputType);
        var circuits = input
            .Select(line => new JBox(line))
            .Select(x => new Circuit([x]))
            .ToList();
        return circuits;
    }

    private static void CalculateDistancesToClosest(List<Circuit> circuits)
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