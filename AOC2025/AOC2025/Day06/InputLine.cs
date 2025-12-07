namespace AOC2025.Day06;

public class InputLine
{
    public InputLine(string line)
    {
        Line = line;
        if (IsEmpty) return;
        var trimmedAndWithoutLastChar = string.Join("", line.SkipLast(1)).Trim();
        Number = int.Parse(trimmedAndWithoutLastChar);
        Op = line.Last() == ' '
            ? null
            : line.Last() == '+'
                ? Operator.Add
                : Operator.Multiply;
    }

    public bool IsEmpty => string.IsNullOrWhiteSpace(Line);
    public string Line { get; set; }
    public long Number { get; set; }
    public Operator? Op { get; set; }
}