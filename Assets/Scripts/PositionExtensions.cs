using UnityEngine;

public static class PositionExtensions
{
    public static Vector2 ToVector2(this Position position, float offset = 1)
    {
        return new Vector2(position.File, position.Rank) * offset;
    }

    public static Position ToPosition(this Vector2 vector, float offset = 1)
    {
        var normalized = vector / offset;
        var rank = Mathf.RoundToInt(normalized.y);
        var file = Mathf.RoundToInt(normalized.x);
        
        return new Position(rank, file);
    }
}