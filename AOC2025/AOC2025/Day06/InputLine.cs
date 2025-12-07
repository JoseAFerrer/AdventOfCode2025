namespace AOC2025.Day06;

public class InputLine
{
    public InputLine(string line)
    {
        var lineWithoutWhitespace = line.Replace("   ", " ").Replace("  ", " ");
        if (lineWithoutWhitespace.Contains('+'))
        {
            LineType = LineType.Operators;
            Operators = lineWithoutWhitespace
                .Split(' ')
                .Where(x => x != string.Empty)
                .ToList();
        }
        else
        { 
            LineType = LineType.Numbers;
            Numbers = lineWithoutWhitespace
                .Split(' ')
                .Where(x => x != string.Empty)
                .Select(int.Parse)
                .ToList();
        }
    }
    public LineType LineType { get; set; }
    public List<int> Numbers { get; set; } = [];
    public List<string> Operators { get; set; } = [];
}

public enum LineType
{
    Numbers,
    Operators,
}