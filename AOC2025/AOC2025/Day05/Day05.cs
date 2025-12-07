using System.Globalization;

namespace AOC2025.Day05;

public class Day05
{
    const int RecursionDepth = 10;
    public static void Solve()
    {
        var freshRanges = GetInitialFreshRanges();
        var curatedRanges = CurateRanges(freshRanges);
        Console.WriteLine($"Curated ranges: {curatedRanges.Count}");


        long freshCounter = 0;
        foreach (var curatedRange in curatedRanges)
        {
            freshCounter += curatedRange.RangeLength;
        }
        
        Console.WriteLine($"Amount of fresh ingredients: {freshCounter}");
    }

    private static List<FreshRange> CurateRanges(List<FreshRange> initialRanges, int recursionDepth = 1)
    {
        if (recursionDepth > RecursionDepth)
        {
            Console.WriteLine("Recursion safety depth exceeded");
            return initialRanges;
        }
        var curatedRanges = new List<FreshRange>(){initialRanges.First()};
        foreach (var range in initialRanges.Except(curatedRanges))
        {
            var curatedRangeThatOverlaps = curatedRanges
                .FirstOrDefault(curatedRange => curatedRange.OverlapsWith(range));

            if (curatedRangeThatOverlaps is null)
            {
                curatedRanges.Add(range);
                continue;
            }
            
            var newMin = Math.Min(range.Min, curatedRangeThatOverlaps.Min);
            var newMax = Math.Max(range.Max, curatedRangeThatOverlaps.Max);
            curatedRanges.Remove(curatedRangeThatOverlaps);
            curatedRanges.Add(new FreshRange(newMin, newMax));
        }

        if (curatedRanges.Any(x => curatedRanges.Any(y => !x.Equals(y) && y.OverlapsWith(x))))
        {
            recursionDepth++;
            curatedRanges = CurateRanges(curatedRanges, recursionDepth);
        }
        return curatedRanges;
    }
    
    private static List<FreshRange> GetInitialFreshRanges()
    {
        var input = FileHelpers.ReadInputLines(5, FileHelpers.Input.Real);
        var freshRanges = new List<FreshRange>();
        foreach (var line in input)
        {
            if(string.IsNullOrWhiteSpace(line))
            { 
                break;
            }

            freshRanges.Add(new FreshRange(line));
        }

        return freshRanges;
    }
}