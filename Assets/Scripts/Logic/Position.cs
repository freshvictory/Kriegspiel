using System;

public struct Position
{
    public int Rank { get; }

    public int File { get; }
    
    public Position(int rank, int file)
    {
        this.Rank = rank;
        this.File = file;
    }
    
    public static Delta Delta(Position origin, Position destination)
    {
        return new Delta(
            rank: Math.Abs(origin.Rank - destination.Rank),
            file: Math.Abs(origin.File - destination.File)
        );
    }

    public static Position Normalize(Position position, PlayerColor color)
    {
        if (color == PlayerColor.Black)
        {
            position = new Position(7, 7) - position;
        }

        return position;
    }

    public static Position operator -(Position first, Position second)
    {
        return new Position(first.Rank - second.Rank, first.File - second.File);
    }

    public static Position operator +(Position first, Position second)
    {
        return new Position(first.Rank + second.Rank, first.File + second.File);
    }
}

public struct Delta
{
    public int Rank { get; }
    
    public int File { get; }
    
    public static readonly Delta Zero = new Delta(0, 0);
    
    public Delta(int rank, int file)
    {
        this.Rank = rank;
        this.File = file;
    }

    public static bool IsHorizontal(Delta delta)
    {
        return delta.File == 0;
    }

    public static bool IsVertical(Delta delta)
    {
        return delta.Rank == 0;
    }

    public static bool IsDiagonal(Delta delta)
    {
        return delta.File == delta.Rank;
    }
    
    public static bool operator <(Delta a, Delta b)
    {
        return a.File < b.File && a.Rank < b.Rank;
    }
    
    public static bool operator >(Delta a, Delta b)
    {
        return a.File > b.File && a.Rank > b.Rank;
    }

    public static bool operator <=(Delta a, Delta b)
    {
        return a.File <= b.File && a.Rank <= b.Rank;
    }
    
    public static bool operator >=(Delta a, Delta b)
    {
        return a.File >= b.File && a.Rank >= b.Rank;
    }

    public static bool operator ==(Delta a, Delta b)
    {
        return a.File == b.File && a.Rank == b.Rank;
    }

    public static bool operator !=(Delta a, Delta b)
    {
        return !(a == b);
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
