namespace AOC2025.Day08;

public class LinkEqualityComparer : IEqualityComparer<Link>
{
    public bool Equals(Link? x, Link? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null) return false;
        if (y is null) return false;
        if (x.GetType() != y.GetType()) return false;

        if (Math.Abs(x.Distance - y.Distance) > 0.01) return false;
        
        var sameButReversed = x.A.Equals(y.B) && x.B.Equals(y.A);
        var exactlyTheSame = x.A.Equals(y.A) && x.B.Equals(y.B) ;
        return sameButReversed || exactlyTheSame;
    }

    public int GetHashCode(Link obj)
    {
        return 0;
    }
}