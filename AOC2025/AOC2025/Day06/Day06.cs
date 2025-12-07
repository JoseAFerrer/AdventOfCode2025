using System.Text;

namespace AOC2025.Day06;

public class Day06
{
    public static void Solve()
    {
        var input = FileHelpers.ReadInputLines(6, FileHelpers.Input.Real);
        var curatedInput = ParseInputLines(input).ToList();
        curatedInput.Add(new InputLine(""));
        var operations = BuildOperations(curatedInput).ToArray();

        var grandTotal = operations.Sum(x => x.Calculate());
        Console.WriteLine($"Grand total of operations: {grandTotal}");
    }

    private static IEnumerable<Operation> BuildOperations(List<InputLine> curatedInput)
    {
        var blankLineIndexes = GetBlankLineIndexes(curatedInput);
        var currentNonBlankLineFirstIndex = 0;
        
        foreach (var index in blankLineIndexes)
        {
            var numbers = new List<long>();
            var currentLines = curatedInput[currentNonBlankLineFirstIndex..index];
            foreach (var line in currentLines)
            {
                numbers.Add(line.Number);
            }
            var op = curatedInput[currentNonBlankLineFirstIndex].Op ?? throw new IndexOutOfRangeException();
            currentNonBlankLineFirstIndex = index +1;
            yield return new Operation(op, numbers.ToArray());
        }
    }

    private static  List<int> GetBlankLineIndexes(List<InputLine> curatedInput)
    {
        var blankLineIndexes = new List<int>();
        for (int i = 0; i < curatedInput.Count; i++)
        {
            if (curatedInput[i].IsEmpty) blankLineIndexes.Add(i);
        }

        return blankLineIndexes;
    }

    private static List<long> GetNumbersForOperationI(InputLine[] curatedInput, int i)
    {
        var numbers = new List<long>();
        for (int j = 0; j < curatedInput.Length-1; j++)
        {
            numbers.Add(curatedInput[j].Number);
        }

        return numbers;
    }

    private static IEnumerable<InputLine> ParseInputLines(string[] input)
    {
        var numberOfLines = input.Length;
        var numberOfHorizontalChars = input.First().Length;
        for (int i = 0; i < numberOfHorizontalChars; i++)
        {
            var sb = new StringBuilder();
            for (int j = 0; j < numberOfLines; j++)
            {
                sb.Append(input[j][i]);
            }

            yield return new InputLine(sb.ToString());
        }
    }
}