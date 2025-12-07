namespace AOC2025.Day06;

public class Operation
{
    public Operation(Operator op, long[] args)
    {
        Op = op;
        Numbers = args;
    }

    public long Calculate() => Op == Operator.Add
        ? Numbers.Sum()
        : Numbers.Aggregate((long)1, (a, b) => a * b);
    public long[] Numbers { get; set; }
    public Operator Op { get; set; }
}

public enum Operator
{
    Add,
    Multiply,
}