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