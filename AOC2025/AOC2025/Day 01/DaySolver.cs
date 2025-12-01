namespace AOC2025.Day_01;

public class DaySolver
{
    public static void Solve()
    {
        var baseLines = File.ReadAllLines("Day 01/sample.txt");
        var operations = baseLines.Select(x =>
        {
            var isAdding = x.StartsWith("R");
            var number = int.Parse(x[1..]);
            return isAdding
                ? number
                : -number;
        }).ToList();

        var init = 50;
        var modulo = 100;
        var counter = 0;
        
        foreach (var operation in operations)
        {
            // Do we end in zero?
            var result = init + operation;
            init = ((result % modulo) + modulo) % modulo;
            if (init == 0)
            {
                counter++;
                Console.WriteLine("Ended in zero");
            }

            Console.WriteLine("result: " + init);
        }

        Console.WriteLine($"Password: {counter}");

    } 
}