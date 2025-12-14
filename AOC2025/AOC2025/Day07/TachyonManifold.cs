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
    public List<World> Worlds { get; set; } = [];

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
            Console.WriteLine($"Completed row {i}/{Rows}. {Worlds.Count} worlds. Time: {elapsed/1000}s");
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
            UpdateWorlds(coords, downNeighborCoords);
            return;
        }

        SplitBeamAroundSplitter(point, coords, downNeighborCoords);
    }

    private void UpdateWorlds(Point2D coords, Point2D downNeighborCoords)
    {
        var relevantWorlds = Worlds.Where(w => w.LastPoint == coords).ToList();
        foreach (var world in relevantWorlds)
            world.Add(downNeighborCoords);
    }

    private void SplitBeamAroundSplitter(TachyonPoint point, Point2D currentCoords, Point2D downNeighborCoords)
    {
        point.BeamSplitWhilePassing = true;
        var westNeighbor = Points[downNeighborCoords.GetWestNeighbor()];
        var eastNeighbor = Points[downNeighborCoords.GetEastNeighbor()];
        westNeighbor.HasBeamPassedThrough = true;
        westNeighbor.Symbol = '|';
        eastNeighbor.HasBeamPassedThrough = true;
        eastNeighbor.Symbol = '|';
        
        var relevantWorlds = Worlds.Where(w => w.LastPoint == currentCoords).ToList();
        foreach (var worldThatContinuesOnWest in relevantWorlds)
        {
            var worldThatContinuesOnEast = new World();
            worldThatContinuesOnEast.Add(downNeighborCoords.GetEastNeighbor());
            Worlds.Add(worldThatContinuesOnEast);
            
            worldThatContinuesOnWest.Add(downNeighborCoords.GetWestNeighbor());
        }
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
        Worlds = [new World(){LastPoint = downNeighborCoords}];

    }
}