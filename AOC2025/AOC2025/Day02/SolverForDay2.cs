using System.Globalization;

namespace AOC2025.Day02;

public static class SolverForDay2
{
    public static void Solve()
    {        
        var input = FileHelpers.ReadInputLines(2, FileHelpers.Input.Sample).First();
        var ranges = ConvertInputToRanges(input).ToArray();

        foreach (var range in ranges)
        {
            if (range.BothNumbersHaveOddNumberOfDigits()) continue;
            
            if (!range.MinOddDigits())
            {
                var stringToCompare = range.Min;
                FindCandidateAndIfGuiltyAddToList(stringToCompare, range);
            }
            
            if (!range.MaxOddDigits())
            {
                var stringToCompare = range.Max;
                FindCandidateAndIfGuiltyAddToList(stringToCompare, range);
            }

            foreach (var guilty in range.GuiltyNumbers)
            {
                Console.WriteLine($"Number {guilty} is guilty");
            }
        }

        var sum = ranges.Sum(range => range.GuiltyNumbers.Sum(long.Parse));
        Console.WriteLine($"Sum of guilty numbers is {sum}");
        
        // 20036447469 too low
    }

    private static void FindCandidateAndIfGuiltyAddToList(string stringToCompare, Range range)
    {
        var half = stringToCompare.Length / 2;
        var firstHalf = stringToCompare[..half];
        var stringCandidate = firstHalf + firstHalf;
        var candidate = long.Parse(stringCandidate);
        var guilty = candidate >= long.Parse(range.Min) && candidate <= long.Parse(range.Max);
        if (guilty)
        {
            range.GuiltyNumbers.Add(stringCandidate);
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