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

    public Delta(VectorComponent rank, VectorComponent file)
    {
        this.Rank = rank;
        this.File = file;
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

    public static Delta operator *(int multiplier, Delta delta)
    {
        return new Delta(multiplier * delta.Rank.Raw, multiplier * delta.File.Raw);
    }
    
    private bool Equals(Delta other)
    {
        return this.Rank.Equals(other.Rank) && this.File.Equals(other.File);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        
        return obj is Delta && this.Equals((Delta)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (this.Rank.GetHashCode() * 397) ^ this.File.GetHashCode();
        }
    }

    public override string ToString()
    {
        return "(" + this.Rank.Raw + ", " + this.File.Raw + ")";
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

    public static VectorComponent operator -(VectorComponent vectorComponent)
    {
        return new VectorComponent(-vectorComponent.Raw);
    }
}

public enum Sign
{
    Negative = -1,
    None = 0,
    Positive = 1,
}