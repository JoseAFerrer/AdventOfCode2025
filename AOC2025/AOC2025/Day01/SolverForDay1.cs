using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2025.Day01;

public class SolverForDay1
{
    public static void Solve()
    {
        var baseLines = FileHelpers.ReadInputLines(1, FileHelpers.Input.Sample);
        var operations = ConvertToOperations(baseLines);

        var start = 50;
        const int modulo = 100;
        var counter = 0;
        
        foreach (var operation in operations)
        {
            var hundreds = Math.Abs(operation) / modulo;
            if (hundreds >= 1)
            {
                Console.WriteLine("hundreds");
                counter += hundreds;
            }
            
            var reducedStart = start % modulo;
            var reducedOperation = operation % modulo;
            var reducedResult = reducedStart + reducedOperation;
            var wentTooHigh = ((reducedStart > 0) && reducedResult > 100) ||
                              ((reducedStart < 0) && reducedResult > 0) ;
            var wentTooLow =  ((reducedStart > 0) && reducedResult < 0) ||
                              ((reducedStart < 0) && reducedResult < -100) ;
            if (wentTooHigh || wentTooLow)
            {
                Console.WriteLine("hello");
                counter++;
            }
            
            var result = start + operation;

            if (result % 100 == 0)
            {
                counter++;
            }
            
            Console.WriteLine(result);
            start = result;
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