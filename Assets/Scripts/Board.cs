public class Board
{
    public static readonly Piece[,] InitialState =
    {
        { new Piece(PieceType.Rook, PlayerColor.White), new Piece(PieceType.Knight, PlayerColor.White), new Piece(PieceType.Bishop, PlayerColor.White), new Piece(PieceType.King, PlayerColor.White), new Piece(PieceType.Queen, PlayerColor.White), new Piece(PieceType.Bishop, PlayerColor.White), new Piece(PieceType.Knight, PlayerColor.White), new Piece(PieceType.Rook, PlayerColor.White) },
        { new Piece(PieceType.Pawn, PlayerColor.White), new Piece(PieceType.Pawn, PlayerColor.White), new Piece(PieceType.Pawn, PlayerColor.White), new Piece(PieceType.Pawn, PlayerColor.White), new Piece(PieceType.Pawn, PlayerColor.White), new Piece(PieceType.Pawn, PlayerColor.White), new Piece(PieceType.Pawn, PlayerColor.White), new Piece(PieceType.Pawn, PlayerColor.White) },
        { Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None },
        { Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None },
        { Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None },
        { Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None },
        { new Piece(PieceType.Pawn, PlayerColor.Black), new Piece(PieceType.Pawn, PlayerColor.Black), new Piece(PieceType.Pawn, PlayerColor.Black), new Piece(PieceType.Pawn, PlayerColor.Black), new Piece(PieceType.Pawn, PlayerColor.Black), new Piece(PieceType.Pawn, PlayerColor.Black), new Piece(PieceType.Pawn, PlayerColor.Black), new Piece(PieceType.Pawn, PlayerColor.Black) },
        { new Piece(PieceType.Rook, PlayerColor.Black), new Piece(PieceType.Knight, PlayerColor.Black), new Piece(PieceType.Bishop, PlayerColor.Black), new Piece(PieceType.King, PlayerColor.Black), new Piece(PieceType.Queen, PlayerColor.Black), new Piece(PieceType.Bishop, PlayerColor.Black), new Piece(PieceType.Knight, PlayerColor.Black), new Piece(PieceType.Rook, PlayerColor.Black) },
    };

    private readonly Piece[,] state;

    public Board()
    {
        this.state = InitialState;
    }

    public Board(Piece[,] state)
    {
        this.state = state;
    }

    public MoveResult Move(Position origin, Position destination)
    {
        if (!Legality.CheckMove(origin, destination, this.state))
        {
            return MoveResult.Illegal;
        }

        var piece = this.state[origin.Rank, origin.File];
        
        this.state[destination.Rank, destination.File] = piece;

        return MoveResult.Legal;

    }
}