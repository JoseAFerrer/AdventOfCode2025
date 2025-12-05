using System.Text;

namespace AOC2025.Day03;

public static class Day03
{
    public static void Solve()
    {        
        var input = FileHelpers.ReadInputLines(3, FileHelpers.Input.Real);
        var banks = input.Select(ConvertInputToLineToBank).ToArray();

        var joltageCounter = 0;
        foreach (var bank in banks)
        {
            joltageCounter += bank.CalculateJoltage();
            Console.WriteLine("Joltage: " + joltageCounter);
        }
        
        Console.WriteLine(joltageCounter);
    }
    
    private static Bank ConvertInputToLineToBank(string input)
    {
        return new Bank(input);
    }
}