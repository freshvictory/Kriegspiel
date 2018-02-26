using System;

public struct Delta
{
    public VectorComponent Rank { get; }
    
    public VectorComponent File { get; }
    
    public static readonly Delta Zero = new Delta(0, 0);

    public Delta(Position origin, Position destination)
    {
        var difference = destination - origin;
        
        this = new Delta(difference.Rank, difference.File);
    }
    
    public Delta(int rank, int file)
    {
        this.Rank = new VectorComponent(rank);
        
        this.File = new VectorComponent(file);
    }

    public static bool IsHorizontal(Delta delta)
    {
        return delta.File.Direction == 0;
    }

    public static bool IsVertical(Delta delta)
    {
        return delta.Rank.Direction == 0;
    }

    public static bool IsDiagonal(Delta delta)
    {
        return delta.File.Magnitude == delta.Rank.Magnitude;
    }

    public static bool operator ==(Delta a, Delta b)
    {
        return a.File.Magnitude == b.File.Magnitude && a.Rank.Magnitude == b.Rank.Magnitude;
    }

    public static bool operator !=(Delta a, Delta b)
    {
        return !(a == b);
    }
}

public struct VectorComponent
{
    public int Raw { get; }

    public int Magnitude { get; }

    public int Direction { get; }

    public VectorComponent(int value)
    {
        this.Raw = value;
        this.Magnitude = Math.Abs(value);
        this.Direction = Math.Sign(value);
    }
}

public enum Sign
{
    Negative = -1,
    None = 0,
    Positive = 1,
}