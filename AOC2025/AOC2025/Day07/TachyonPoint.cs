namespace AOC2025.Day07;

public class TachyonPoint
{
    public TachyonPoint(char symbol)
    {
        Symbol = symbol;
        switch (symbol)
        {
            case 'S':
                IsStartingPoint = true;
                IsSpace = false;
                break;
            case '^':
                IsSplitter = true;
                IsSpace = false;
                break;
        }
    }

    public char Symbol { get; set; }
    public bool IsStartingPoint { get; set; }
    public bool IsSplitter { get; set; }
    public bool IsSpace { get; set; } = true;
    public bool HasBeamPassedThrough { get; set; }
    public bool IsChecked { get; set; }
    public bool BeamSplitWhilePassing { get; set; }
    public bool WorldsCalculated { get; set; }
}