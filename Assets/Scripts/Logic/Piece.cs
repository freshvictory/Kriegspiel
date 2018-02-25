using System;

public struct Piece {

    public PieceType Type { get; }

    public PlayerColor Color { get; }
    
    public bool Moved { get; set; }

    public static readonly Piece None = new Piece(PieceType.None, PlayerColor.None);

    public Piece(PieceType type, PlayerColor color) {
		this.Type = type;
        this.Color = color;
        this.Moved = false;
    }
}

public enum PieceType {
    None = 0,
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}

public static class PieceTypeExtensions
{
    public static string Display(this PieceType pieceType)
    {
        switch (pieceType)
        {
            case PieceType.Pawn:
                return "P";
            case PieceType.Rook:
                return "R";
            case PieceType.Knight:
                return "N";
            case PieceType.Bishop:
                return "B";
            case PieceType.Queen:
                return "Q";
            case PieceType.King:
                return "K";
            case PieceType.None:
                return " ";
            default:
                throw new ArgumentOutOfRangeException(nameof(pieceType), pieceType, null);
        }
    }
}

public enum PlayerColor {
    None = 0,
    White,
    Black
}

public static class PlayerColorExtensions {
    public static PlayerColor Enemy(this PlayerColor playerColor)
    {
        if (playerColor == PlayerColor.None)
        {
            return PlayerColor.None;
        }

        return playerColor == PlayerColor.White
             ? PlayerColor.Black
             : PlayerColor.White;
    }
}
