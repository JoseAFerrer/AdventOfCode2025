namespace AOC2025.Day07;

public class Day07
{
    public static void Solve()
    {
        var input = FileHelpers.ReadInputLines(7, FileHelpers.Input.Real);
        var manifold = new TachyonManifold(input);
        manifold.SendTachyonBeam();

        manifold.Print();
        var split = manifold.Points.Values.Count(x => x.BeamSplitWhilePassing);
        Console.WriteLine($"The beam split {split} times");
        
        var worldCount = manifold.WorldCount;
        Console.WriteLine($"This manifold generates {worldCount} worlds");
    }
}