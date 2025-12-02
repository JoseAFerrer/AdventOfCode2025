using System.IO;

namespace AOC2025;

public static class FileHelpers
{
    public static string[] ReadInputLines(int day, Input input)
    {
        const string sampleString = "sample";
        const string inputString = "input";
        var stringedDay = day.ToString("D2");
        var whatToRead = input == Input.Sample ?  sampleString : inputString;
        var whereToRead = $"Day{stringedDay}/{whatToRead}.txt";
        var baseLines = File.ReadAllLines(whereToRead);
        return baseLines;
    }
    
    public enum Input
    {
        Sample,
        Real
    }
}