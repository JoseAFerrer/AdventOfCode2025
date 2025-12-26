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

        foreach (var link in links.Take(numberOfCables))
        {
            var a = link.A;
            var circuitA = circuits.FirstOrDefault(x => x.Boxes.Contains(a))!;
            var b = link.B;
            var circuitB = circuits.FirstOrDefault(x => x.Boxes.Contains(b))!;

            if (circuitA.Id == circuitB.Id) continue;
            
            Console.WriteLine($"Combining circuits with boxes {circuitA.BoxesLocations} and {circuitB.BoxesLocations} with distance {link.Distance}");
            CombineAndRemoveSecond(circuitA, circuitB, circuits);
        }

        Console.WriteLine("Number of circuits: " + circuits.Count);
    }

    private static void CombineAndRemoveSecond(Circuit firstCircuit, Circuit secondCircuit, List<Circuit> circuits)
    {
        firstCircuit.Boxes.AddRange(secondCircuit.Boxes);
        circuits.Remove(secondCircuit);
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