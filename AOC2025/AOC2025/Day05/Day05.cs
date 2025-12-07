using System.Globalization;

namespace AOC2025.Day05;

public class Day05
{
    public static void Solve()
    {
        var (freshRanges, ingredientIdAndIsFreshDict) = GetRangesAndIngredients();

        foreach (var (id, _) in ingredientIdAndIsFreshDict)
        {
            var isFresh = freshRanges.Any(x => x.IsIngredientFresh(id));
            ingredientIdAndIsFreshDict[id] = isFresh;
        }
        
        Console.WriteLine($"Ranges: {freshRanges.Count}");
        Console.WriteLine($"Fresh ingredients: {ingredientIdAndIsFreshDict.Count(x => x.Value)}");
    }

    private static (List<FreshRange> freshRanges, Dictionary<long, bool> ingredientIds) GetRangesAndIngredients()
    {
        var input = FileHelpers.ReadInputLines(5, FileHelpers.Input.Real);
        var freshRanges = new List<FreshRange>();
        var ingredientAndIsFresh = new Dictionary<long, bool>();
        var lineIsARange = true;
        foreach (var line in input)
        {
            if(string.IsNullOrWhiteSpace(line))
            {
                lineIsARange = false;
                continue;
            }
            
            if (lineIsARange)
            {
                freshRanges.Add(new FreshRange(line));
                continue;
            }
            
            ingredientAndIsFresh.Add(long.Parse(line), false);
        }

        return (freshRanges, ingredientAndIsFresh);
    }
}