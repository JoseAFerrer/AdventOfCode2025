namespace AOC2025.Day02;

public class Range(string min, string max)
{
    public string Min { get; } = min;
    public string Max { get; } = max;
    public bool MinOddDigits() => Min.Length % 2 == 1;
    public bool MaxOddDigits() => Max.Length % 2 == 1;
    public HashSet<string> GuiltyNumbers { get; set; } = [];
    
    public bool BothNumbersHaveOddNumberOfDigits()
    {
        return MinOddDigits() && MaxOddDigits();
    }
}