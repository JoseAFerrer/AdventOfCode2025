namespace AOC2025.Day08;

public class Link(JBox a, JBox b, double d)
{
    public JBox A { get; set; } = a;
    public JBox B { get; set; } = b;
    public double Distance { get; set; } = d;
}