namespace AOC2025.Day_01;

public class DaySolver
{
    public static void Solve()
    {
        var baseLines = File.ReadAllLines("Day 01/sample.txt");
        var operations = ConvertToOperations(baseLines);

        var start = 50;
        var modulo = 100;
        var counter = 0;
        
        foreach (var operation in operations)
        {
            // whole loops given by operation
            var wholeLoops = operation / 100;
            counter += wholeLoops;

            Console.WriteLine("Whole loops: " + wholeLoops);
            
            // possible extra loop given by operation
            var rest = ((operation % modulo) + modulo) % modulo;
            var partialResult = start + (rest * (operation > 0 ? 1 : -1));
            var partialPassedThroughZero = partialResult is < 0 or > 100;
            if (partialPassedThroughZero)
            {
                counter++;
                Console.WriteLine("Extra loop");
            }

            // Do we end in zero?
            var result = start + operation;
            start = ((result % modulo) + modulo) % modulo;
            if (start == 0)
            {
                counter++;
                Console.WriteLine("Ended in zero");
            }

            Console.WriteLine("result: " + start);
        }

        Console.WriteLine($"Password: {counter}");

    }

    private static List<int> ConvertToOperations(string[] baseLines)
    {
        var operations = baseLines.Select(x =>
        {
            var isAdding = x.StartsWith("R");
            var number = int.Parse(x[1..]);
            return isAdding
                ? number
                : -number;
        }).ToList();
        return operations;
    }
}