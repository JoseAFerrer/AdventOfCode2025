namespace AOC2025.Day01;

public static class SolverForDay1
{
    public static void Solve()
    {
        var baseLines = FileHelpers.ReadInputLines(1, FileHelpers.Input.Real);
        var operations = ConvertToOperations(baseLines);

        var start = 50;
        const int modulo = 100;
        var counter = 0;
        
        foreach (var operation in operations)
        {
            counter = CountTimesModuloIsContainedInOperation(operation, modulo, counter);

            counter = CountIfOperationChangedRangeWithModulo(start, modulo, operation, counter);

            var result = start + operation;

            counter = CheckIfResultIsModuloZero(result, counter);
            start = result;
        }

        Console.WriteLine($"Password: {counter}");

    }

    private static int CountTimesModuloIsContainedInOperation(int operation, int modulo, int counter)
    {
        var hundreds = Math.Abs(operation) / modulo;
        if (hundreds < 1) return counter;
        
        counter += hundreds;
        return counter;
    }

    private static int CountIfOperationChangedRangeWithModulo(int start, int modulo, int operation, int counter)
    {
        var reducedStart = start % modulo;
        var reducedOperation = operation % modulo;
        var reducedResult = reducedStart + reducedOperation;
        var wentTooHigh = (reducedStart > 0 && reducedResult > 100) ||
                          (reducedStart < 0 && reducedResult > 0) ;
        var wentTooLow =  (reducedStart > 0 && reducedResult < 0) ||
                          (reducedStart < 0 && reducedResult < -100) ;
        if (wentTooHigh || wentTooLow)
        {
            counter++;
        }

        return counter;
    }

    private static int CheckIfResultIsModuloZero(int result, int counter)
    {
        if (result % 100 == 0)
        {
            counter++;
        }

        return counter;
    }

    private static List<int> ConvertToOperations(string[] baseLines)
    {
        var operations = baseLines.Select(x =>
        {
            var isAdding = x.StartsWith('R');
            var number = int.Parse(x[1..]);
            return isAdding
                ? number
                : -number;
        }).ToList();
        return operations;
    }
}