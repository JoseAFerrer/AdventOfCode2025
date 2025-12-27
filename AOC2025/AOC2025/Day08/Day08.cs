using System.Diagnostics;
using AOC2025.Common;

namespace AOC2025.Day08;

public class Day08
{
    
    public static void Solve()
    {
        var sw =  Stopwatch.StartNew();
        const int numberOfCables = 10000;
        var (circuits, links) = GetLinksAndCircuits(FileHelpers.Input.Real, numberOfCables);

        var handledLink = links.Last();
        Console.WriteLine("Elapsed time: " + sw.ElapsedMilliseconds);
        foreach (var link in links.Take(numberOfCables))
        {
            handledLink = HandleLink(link, circuits);
            if (circuits.Count == 1) break;
        }

        var x1 = handledLink.A.Point.X;
        var x2 = handledLink.B.Point.X;
        Console.WriteLine($"Last 2 boxes x coordinates: {x1}, {x2}");
        Console.WriteLine($"Distance from the wall: {x1*x2}");
        
        // 1277378937 too low
        // 3200955921
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

    private static Link HandleLink(Link link, List<Circuit> circuits)
    {
        var a = link.A;
        var circuitA = circuits.FirstOrDefault(x => x.Boxes.Contains(a))!;
        var b = link.B;
        var circuitB = circuits.FirstOrDefault(x => x.Boxes.Contains(b))!;

        if (circuitA.Id == circuitB.Id) return link;
        CombineAndRemoveSecond(circuitA, circuitB, circuits);
        return link;
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