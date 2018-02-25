using System;
using System.Collections.Generic;
using System.Linq;

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

    public static Position operator -(Position first, Position second)
    {
        return new Position(first.Rank - second.Rank, first.File - second.File);
    }

    public static Position operator +(Position first, Position second)
    {
        return new Position(first.Rank + second.Rank, first.File + second.File);
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
