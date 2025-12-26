using AOC2025.Common;

namespace AOC2025.Day08;

public class Day08
{
    
    public static void Solve()
    {
        const FileHelpers.Input inputType = FileHelpers.Input.Sample;
        var circuits = GetCircuits(inputType);
        const int numberOfCables = 10;
        CalculateDistancesToClosest(circuits);
        var maxDistance = circuits.Max(x => x.DistanceToClosest);

        for (var i = 0; i < numberOfCables; i++)
        {
            var minDistance = circuits.Min(x => x.DistanceToClosest);
            
            var firstCircuit = circuits.First(x => x.DistanceToClosest <= minDistance);
            var secondCircuit = circuits.First(x => x.Id == firstCircuit.ClosestCircuitId);
            
            CombineAndRemoveSecond(firstCircuit, secondCircuit, circuits);
        }
    }

    private static void CombineAndRemoveSecond(Circuit firstCircuit, Circuit secondCircuit, List<Circuit> circuits)
    {
        Console.WriteLine($"Combining circuits with boxes {firstCircuit.BoxesLocations} and {secondCircuit.BoxesLocations} with distance {firstCircuit.DistanceToClosest}");
        firstCircuit.Boxes.AddRange(secondCircuit.Boxes);
        circuits.Remove(secondCircuit);
        firstCircuit.DistanceToClosest = circuits.Max(x => x.DistanceToClosest);
        foreach (var iteratingCircuit in circuits.Except([firstCircuit, secondCircuit]))
        {
            if (iteratingCircuit.ClosestCircuitId != firstCircuit.Id
                && iteratingCircuit.ClosestCircuitId != secondCircuit.Id) continue;
            
            iteratingCircuit.ClosestCircuitId = firstCircuit.Id;
            if (!(iteratingCircuit.DistanceToClosest <= firstCircuit.DistanceToClosest)) continue;
            
            firstCircuit.DistanceToClosest = iteratingCircuit.DistanceToClosest;
            firstCircuit.ClosestCircuitId = iteratingCircuit.Id;
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