using AOC2025.Common;

namespace AOC2025.Day08;

public class Day08
{
    
    public static void Solve()
    {
        const FileHelpers.Input inputType = FileHelpers.Input.Sample;
        var circuits = GetCircuits(inputType);
        var links = CalculateDistances(circuits)
            .Distinct(new LinkEqualityComparer())
            .OrderBy(x => x.Distance)
            .ToList();

        const int numberOfCables = 10;
        for (var i = 0; i < numberOfCables; i++)
        {
            if (circuits.Count == 1)
            {
                break;
            }
            var minDistance = circuits.Min(x => x.DistanceToClosest);
            
            var firstCircuit = circuits.First(x => x.DistanceToClosest <= minDistance);
            var secondCircuit = circuits.First(x => x.Id == firstCircuit.ClosestCircuitId);
            
            CombineAndRemoveSecond(firstCircuit, secondCircuit, circuits);
        }

        Console.WriteLine("Number of circuits: " + circuits.Count);
    }

    private static void CombineAndRemoveSecond(Circuit firstCircuit, Circuit secondCircuit, List<Circuit> circuits)
    {
        Console.WriteLine($"Combining circuits with boxes {firstCircuit.BoxesLocations} and {secondCircuit.BoxesLocations} with distance {firstCircuit.DistanceToClosest}");
        firstCircuit.Boxes.AddRange(secondCircuit.Boxes);
        circuits.Remove(secondCircuit);
        var tempD = 1000.0;
        firstCircuit.DistanceToClosest = tempD;
        firstCircuit.ClosestCircuitId = "";
        foreach (var current in circuits.Except([firstCircuit]))
        {
            if (current.ClosestCircuitId == secondCircuit.Id) current.ClosestCircuitId = firstCircuit.Id;
            
            if (current.ClosestCircuitId == firstCircuit.Id &&
                current.DistanceToClosest > firstCircuit.DistanceToClosest) 
                continue;

            var distanceToFirst = current.DistanceTo(firstCircuit);
            if (distanceToFirst < firstCircuit.DistanceToClosest)
            {
                firstCircuit.SetNewClosestCircuit(current.Id, distanceToFirst);
            }
            
            if (distanceToFirst < current.DistanceToClosest)
            {
                current.SetNewClosestCircuit(firstCircuit.Id, distanceToFirst);
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

    private static IEnumerable<Link> CalculateDistances(List<Circuit> circuits)
    {
        foreach (var a in circuits)
        {
            var others = circuits.Except([a]);
            foreach (var b in others)
            {
                var link = new Link(a.Boxes.First(), b.Boxes.First());
                yield return link;
            }
        }
    }
}
}