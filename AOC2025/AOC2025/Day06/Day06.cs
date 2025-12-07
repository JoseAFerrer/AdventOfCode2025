namespace AOC2025.Day06;

public class Day06
{
    public static void Solve()
    {
        var input = FileHelpers.ReadInputLines(6, FileHelpers.Input.Real);
        var curatedInput = ParseInputLines(input).ToArray();
        var operations = BuildOperations(curatedInput).ToArray();

        var grandTotal = operations.Sum(x => x.Calculate());
        Console.WriteLine($"Grand total of operations: {grandTotal}");
    }

    private static IEnumerable<Operation> BuildOperations(InputLine[] curatedInput)
    {
        var numberOfOperations = curatedInput.First().Numbers.Count;
        var operators = curatedInput.Last().Operators;

        for (var i = 0; i < numberOfOperations; i++)
        {
            var numbers = GetNumbersForOperationI(curatedInput, i);
            var op = operators[i] == "+" ? Operator.Add : Operator.Multiply;
            yield return (new Operation(op, numbers.ToArray()));
        }
    }

    private static List<long> GetNumbersForOperationI(InputLine[] curatedInput, int i)
    {
        var numbers = new List<long>();
        for (int j = 0; j < curatedInput.Length-1; j++)
        {
            numbers.Add(curatedInput[j].Numbers[i]);
        }

        return numbers;
    }

    private static IEnumerable<InputLine> ParseInputLines(string[] input)
    {
        return input.Select(t => new InputLine(t));
    }
}