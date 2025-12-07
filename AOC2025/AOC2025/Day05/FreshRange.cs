namespace AOC2025.Day05;

public class FreshRange
{
    public FreshRange(string input)
    {
        var parts = input.Split('-');
        Min = long.Parse(parts[0]);
        Max = long.Parse(parts[1]);
    }
    
    public FreshRange(long min, long max)
    {
        Min = min;
        Max = max;
    }
    
    public bool IsInRange(long ing) => ing >= Min && ing <= Max;
    public bool OverlapsWith(FreshRange r) => IsInRange(r.Min) || IsInRange(r.Max) || r.IsInRange(Min) || r.IsInRange(Max);
    public bool Equals(FreshRange r) => r.Min == Min && r.Max == Max;
    public long RangeLength => Max - Min + 1;
    public long Min { get; set; }
    public long Max { get; set; }
}