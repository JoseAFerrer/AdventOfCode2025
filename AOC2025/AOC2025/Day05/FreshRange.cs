namespace AOC2025.Day05;

public class FreshRange
{
    public FreshRange(string input)
    {
        var parts = input.Split('-');
        Min = long.Parse(parts[0]);
        Max = long.Parse(parts[1]);
    }
    
    public bool IsIngredientFresh(long ing) => ing >= Min && ing <= Max;
    public long Min { get; set; }
    public long Max { get; set; }
}