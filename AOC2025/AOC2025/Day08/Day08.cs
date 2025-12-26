using System.Diagnostics;
using AOC2025.Common;

namespace AOC2025.Day08;

public class Day08
{
    
    public static void Solve()
    {
        var sw =  Stopwatch.StartNew();
        const int numberOfCables = 1000;
        var (circuits, links) = GetLinksAndCircuits(FileHelpers.Input.Real, numberOfCables);

        Console.WriteLine("Elapsed time: " + sw.ElapsedMilliseconds);
        foreach (var link in links.Take(numberOfCables))
        {
            HandleLink(link, circuits);
        }

        Console.WriteLine("Number of circuits: " + circuits.Count);
        var orderedCircuits = circuits.OrderByDescending(x => x.Boxes.Count).ToArray();
        Console.WriteLine("Circuit index: " 
                          + orderedCircuits[0].Boxes.Count 
                          * orderedCircuits[1].Boxes.Count
                          * orderedCircuits[2].Boxes.Count);
    }

    private static (List<Circuit> circuits, List<Link> links) GetLinksAndCircuits(FileHelpers.Input inputType, int numberOfCables)
    {
        var circuits = GetCircuits(inputType);
        var links = CalculateDistances(circuits, numberOfCables)
            .OrderBy(x => x.Distance)
            .Take(numberOfCables)
            .ToList();
        return (circuits, links);
    }

    private static void HandleLink(Link link, List<Circuit> circuits)
    {
        var a = link.A;
        var circuitA = circuits.FirstOrDefault(x => x.Boxes.Contains(a))!;
        var b = link.B;
        var circuitB = circuits.FirstOrDefault(x => x.Boxes.Contains(b))!;

        if (circuitA.Id == circuitB.Id) return;
        CombineAndRemoveSecond(circuitA, circuitB, circuits);
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

    private static IEnumerable<Link> CalculateDistances(List<Circuit> circuits, int numberOfCables)
    {
        var checkedKeys = new HashSet<(string a, string b)>();
        var currentMax = 0.0;
        var length = 0;
        var finalList = new List<Link>();
        foreach (var a in circuits)
        {
            var others = circuits.Except([a]);
            foreach (var b in others)
            {
                if (checkedKeys.Contains((b.BoxesLocations, a.BoxesLocations))) continue;
                var d = a.Boxes.First().Point.DistanceTo(b.Boxes.First().Point);
                if (d > currentMax && length > numberOfCables) continue;
                
                length++;
                currentMax = Math.Max(d, currentMax);
                checkedKeys.Add((a.BoxesLocations, b.BoxesLocations));
                finalList.Add(new Link(a.Boxes.First(), b.Boxes.First(), d));

                if (finalList.Count < numberOfCables * 4) continue;
                
                finalList = finalList.OrderBy(x => x.Distance).Take(numberOfCables).ToList();
                currentMax = finalList.Last().Distance;
            }
        }
        return finalList;
    }
}