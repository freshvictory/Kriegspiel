using System.Collections.Generic;
using System.Linq;

public struct MoveRule
{
    public List<Delta> Deltas { get; }
    
    public int MaxMultiplier { get; }

    public MoveRule(Delta delta, int maxMultiplier)
    {
        this.MaxMultiplier = maxMultiplier;
        this.Deltas = GetDeltas(delta);
    }

    private static List<Delta> GetDeltas(Delta baseDelta)
    {
        var deltas = new List<Delta>
        {
            baseDelta,
            new Delta(baseDelta.Rank, -baseDelta.File),
            new Delta(-baseDelta.Rank, baseDelta.File),
            new Delta(-baseDelta.Rank, -baseDelta.File)
        };

        return deltas.Distinct().ToList();
    }
}