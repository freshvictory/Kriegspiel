using System;
using System.Collections.Generic;

public struct Position
{
    public int Rank { get; }

    public int File { get; }
    
    public Position(int rank, int file)
    {
        this.Rank = rank;
        this.File = file;
    }

    public static Position Normalize(Position position, PlayerColor color)
    {
        if (color == PlayerColor.Black)
        {
            position = new Position(7, 7) - position;
        }

        return position;
    }

    public static IEnumerable<Position> GetRange(Position origin, Delta delta)
    {
        if (delta == Delta.Zero)
        {
            yield return origin;
        }

        if (!(Delta.IsDiagonal(delta) || Delta.IsHorizontal(delta) || Delta.IsVertical(delta)))
        {
            throw new InvalidOperationException("Range must be diagonal, horizontal, or vertical.");
        }
        
        for (int i = 1; i < Math.Max(delta.Rank.Magnitude, delta.File.Magnitude); i++)
        {
            yield return origin + new Delta(delta.Rank.Direction * i, delta.File.Direction * i);
        }
    }

    public static Position operator -(Position first, Position second)
    {
        return new Position(first.Rank - second.Rank, first.File - second.File);
    }

    public static Position operator +(Position first, Position second)
    {
        return new Position(first.Rank + second.Rank, first.File + second.File);
    }

    public static Position operator +(Position position, Delta delta)
    {
        return new Position(position.Rank + delta.Rank.Raw, position.File + delta.File.Raw);
    }

    public static bool operator ==(Position first, Position second)
    {
        return first.Rank == second.Rank && first.File == second.File;
    }

    public static bool operator !=(Position first, Position second)
    {
        return !(first == second);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        return obj is Position && this.Equals((Position) obj);
    }
    
    private bool Equals(Position other)
    {
        return this.Rank == other.Rank && this.File == other.File;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (this.Rank * 397) ^ this.File;
        }
    }
}

public enum File {
    a = 0,
    b = 1,
    c = 2,
    d = 3,
    e = 4,
    f = 5,
    g = 6,
    h = 7,
}
