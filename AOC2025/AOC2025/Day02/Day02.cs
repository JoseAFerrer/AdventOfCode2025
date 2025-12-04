using System.Globalization;
using System.Text;

namespace AOC2025.Day02;

public static class Day02
{
    public static void Solve()
    {        
        var input = FileHelpers.ReadInputLines(2, FileHelpers.Input.Real).First();
        var ranges = ConvertInputToRanges(input).ToArray();

        foreach (var range in ranges)
        {
            HandleRange(range);
        }

        var sum = ranges.Sum(range => range.GuiltyNumbers.Sum(long.Parse));
        Console.WriteLine($"Sum of guilty numbers is {sum}");
        
        // 29818212493 is correct for part 1
    }

    private static void HandleRange(Range range)
    {
        for (var current = range.LongMin; current < range.LongMax+1; current++)
        {
            var digits = current.ToString().Length;
            var currentString = current.ToString();
            for (var j = 1; j <= digits / 2; j++)
            {
                var repeatedSection = currentString[..j];
                var repeatCount = digits / j;

                var agg = new StringBuilder();
                for (var i = 0; i < repeatCount; i++)
                {
                    agg = agg.Append(repeatedSection);
                }

                if (agg.ToString() == currentString)
                {
                    range.AddGuiltyToListIfInRange(currentString);
                }
            }
        }

        foreach (var guilty in range.GuiltyNumbers)
        {
            Console.WriteLine($"Number {guilty} is guilty");
        }
    }

    private static IEnumerable<Range> ConvertInputToRanges(string input)
    {
        var ranges = input.Split(',').Select(x =>
        {
            var minmax = x.Split('-');
            return new Range(minmax[0], minmax[1]);
        });
        return ranges;
    }
}