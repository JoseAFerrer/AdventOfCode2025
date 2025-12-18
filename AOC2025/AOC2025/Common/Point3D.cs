namespace AOC2025.Common;

public class Point3D
{
    public Point3D(string commaSeparatedLongs)
    {
        var chunks = commaSeparatedLongs.Split(',');
        X  = long.Parse(chunks[0]);
        Y = long.Parse(chunks[1]);;
        Z = long.Parse(chunks[2]);;
    }
    
    public Point3D(long x, long y, long z)
    {
        X  = x;
        Y = y;
        Z = z;
    }

    public double DistanceTo(Point3D other)
    {
        return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2) + Math.Pow(Z - other.Z, 2));
    }

    public long X { get; set; }
    public long Y { get; set; }
    public long Z { get; set; }
    
}