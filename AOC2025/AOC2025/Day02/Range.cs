namespace AOC2025.Day02;

public class Range(string min, string max)
{
    public string Min { get; } = min;
    public long LongMin { get; } = long.Parse(min);
    public string Max { get; } = max;
    public long LongMax { get; } = long.Parse(max);
    public bool MinOddDigits() => Min.Length % 2 == 1;
    public bool MaxOddDigits() => Max.Length % 2 == 1;
    public HashSet<string> GuiltyNumbers { get; set; } = [];

    public long? GetFirstEvenDigitNumberInRange()
    {
        if (Min.Length == Max.Length && MinOddDigits()) return null;
        
        if (!MinOddDigits())
        {
            return LongMin;
        }

        var numberOfDigitsOfMin = (long)Math.Floor(Math.Log10(LongMin) + 1);
        var nextPowerOfTen = (long)Math.Pow(10, numberOfDigitsOfMin);
        return LongMax >= nextPowerOfTen
            ? nextPowerOfTen
            : null; 
    }
    
    public bool BothNumbersHaveOddNumberOfDigits()
    {
        return MinOddDigits() && MaxOddDigits();
    }
    
    public void AddGuiltyToListIfInRange(string guiltyCandidate)
    {
        var candidate = long.Parse(guiltyCandidate);
        var guilty = candidate >= long.Parse(Min) && candidate <= long.Parse(Max);
        if (guilty)
        {
            GuiltyNumbers.Add(guiltyCandidate);
        }
    }
}