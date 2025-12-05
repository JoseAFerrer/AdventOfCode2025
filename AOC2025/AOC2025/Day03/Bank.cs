namespace AOC2025.Day03;

public class Bank
{
    public Bank(string line)
    {
        Batteries = line.Select(x => (int)char.GetNumericValue(x)).ToList();
        BatteriesWithoutFirstNumber = Batteries.Skip(1).ToArray();
        BatteriesWithoutLastNumber = Batteries.SkipLast(1).ToArray();
    }

    public List<int> Batteries { get; set; }
    public int[] BatteriesWithoutFirstNumber { get; set; }
    public int[] BatteriesWithoutLastNumber { get; set; }

    public int CalculateJoltage()
    {
        var highestNumberExceptLast = BatteriesWithoutLastNumber.Max();
        var indexOfHighest = Batteries.IndexOf(highestNumberExceptLast);
        var secondHighestNumberAfterFirst = Batteries.Skip(indexOfHighest + 1).Max();
        return int.Parse(highestNumberExceptLast.ToString() + secondHighestNumberAfterFirst);
    }
}