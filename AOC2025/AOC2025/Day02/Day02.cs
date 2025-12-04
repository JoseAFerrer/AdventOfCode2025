using System.Globalization;

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
        // this might ignore some valid cases if the range is something like 8-100 (11, 22, 33...),
        // but it is unlikely this will happen for large numbers. It might be worth considering though.
        if (range.BothNumbersHaveOddNumberOfDigits()) return;

        var overMax = false; 
        var current = range.GetFirstEvenDigitNumberInRange();
        var currentAsString = current.ToString();
        if (current is null) overMax = true;
        while (!overMax)
        {
            var half = currentAsString!.Length / 2;
            var firstHalf = currentAsString[..half];
            var guiltyCandidate = firstHalf + firstHalf;
            range.AddGuiltyToListIfInRange(guiltyCandidate);
            
            // 78 -> "7" -> "8" -> 88 < max? test and yes? repeat. no? end
            var nextFirstHalf = long.Parse(firstHalf)+1;
            currentAsString = nextFirstHalf.ToString() + nextFirstHalf;

            if (long.Parse(currentAsString) > range.LongMax)
            {
                overMax = true;
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