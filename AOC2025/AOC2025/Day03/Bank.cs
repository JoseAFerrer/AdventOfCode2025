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

    public int CalculateSimpleJoltage()
    {
        var highestNumberExceptLast = BatteriesWithoutLastNumber.Max();
        var indexOfHighest = Batteries.IndexOf(highestNumberExceptLast);
        var secondHighestNumberAfterFirst = Batteries.Skip(indexOfHighest + 1).Max();
        return int.Parse(highestNumberExceptLast.ToString() + secondHighestNumberAfterFirst);
    }
    
    public long CalculateComplexJoltage()
    {
        var batteriesToWorkWith = Batteries;
        var batteriesToStoreResult = new int[Batteries.Count];

        for (int i = 0; i < 12; i++)
        {
            var currentHighestNumber = batteriesToWorkWith.Max(); 
            var indexOfHighest = batteriesToWorkWith.LastIndexOf(currentHighestNumber);
            batteriesToStoreResult[indexOfHighest] = currentHighestNumber;
            batteriesToWorkWith[indexOfHighest] = 0;
        }

        var joltageAsString = string.Concat(batteriesToStoreResult.Select(x => x != 0 
            ? x.ToString() 
            : string.Empty));
        return long.Parse(joltageAsString);
    }
}