using System.Diagnostics;
using AOC2025.Common;

namespace AOC2025.Day07;

public class TachyonManifold
{
    public TachyonManifold(string[] input)
    {
        var reversedInput = input.Reverse().ToArray();
        Rows = input.Length;
        Columns = input.First().Length;
        for (var i = 0; i < Rows; i++)
        {
            var line = reversedInput[i];
            for (var j = 0; j < Columns; j++)
            {
                var point = new TachyonPoint(line[j]);
                Points.Add(new Point2D(j, i), point);
            }
        }
    }

    public int Rows { get; set; }
    public int Columns { get; set; }
    public Dictionary<Point2D, TachyonPoint> Points { get; set; } = [];
    public long WorldCount => Points.Where(p => p.Key.Y == 0).Sum(p => p.Value.WorldsWhereParticlePassesThroughHere);

    public void SendTachyonBeam()
    {
        InitiateBeam();

        for (var i = 0; i < Rows; i++)
        {
            var sw = Stopwatch.StartNew();
            var pointsToCheck = Points.Where(x => 
                x.Value is { HasBeamPassedThrough: true, IsChecked: false });
            
            foreach (var (coords, point) in pointsToCheck) 
                SendBeamDownwards(coords, point);

            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;
            Console.WriteLine($"Completed row {i}/{Rows}. Time: {elapsed/1000}s");
        }
    }

    private void SendBeamDownwards(Point2D coords, TachyonPoint point)
    {
        point.IsChecked = true;
        var downNeighborCoords = coords.GetDownNeighbor();
        var downNeighborExists = Points.TryGetValue(downNeighborCoords, out var nextDownNeighbor);
        if (!downNeighborExists || nextDownNeighbor is null) return;

        if (!nextDownNeighbor.IsSplitter)
        {
            nextDownNeighbor.HasBeamPassedThrough = true;
            nextDownNeighbor.Symbol = '|';
            nextDownNeighbor.WorldsWhereParticlePassesThroughHere += point.WorldsWhereParticlePassesThroughHere;
            return;
        }

        SplitBeamAroundSplitter(point, downNeighborCoords);
    }

    private void SplitBeamAroundSplitter(TachyonPoint point, Point2D downNeighborCoords)
    {
        point.BeamSplitWhilePassing = true;
        var westNeighbor = Points[downNeighborCoords.GetWestNeighbor()];
        var eastNeighbor = Points[downNeighborCoords.GetEastNeighbor()];
        westNeighbor.HasBeamPassedThrough = true;
        westNeighbor.Symbol = '|';
        westNeighbor.WorldsWhereParticlePassesThroughHere += point.WorldsWhereParticlePassesThroughHere;
        eastNeighbor.HasBeamPassedThrough = true;
        eastNeighbor.Symbol = '|';
        eastNeighbor.WorldsWhereParticlePassesThroughHere += point.WorldsWhereParticlePassesThroughHere;
    }

    private void InitiateBeam()
    {
        var start = Points
            .First(x => x.Value.IsStartingPoint);
        var downNeighborCoords = start.Key.GetDownNeighbor();
        var downNeighborExists = Points.TryGetValue(downNeighborCoords, out var downNeighbor);
        if (!downNeighborExists || downNeighbor == null) throw new ArgumentException();
        downNeighbor.HasBeamPassedThrough = true;
        downNeighbor.Symbol = '|';
        downNeighbor.WorldsWhereParticlePassesThroughHere = 1;
    }
}