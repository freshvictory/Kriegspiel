public struct Piece {

    public PieceType Type { get; }

    public PlayerColor Color { get; }

    public static readonly Piece None = new Piece(PieceType.None, PlayerColor.None);

    public Piece(PieceType type, PlayerColor color) {
		this.Type = type;
        this.Color = color;
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
};

public enum PlayerColor {
    None = 0,
    White,
    Black
}
