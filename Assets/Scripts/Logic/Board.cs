using System.Collections.Generic;
using System.Linq;

public class Board
{
    private readonly Piece[,] board;
    
    private static readonly Piece[,] InitialBoard =
    {
        { new Piece(PieceType.Rook, PlayerColor.White), new Piece(PieceType.Knight, PlayerColor.White), new Piece(PieceType.Bishop, PlayerColor.White), new Piece(PieceType.Queen, PlayerColor.White), new Piece(PieceType.King, PlayerColor.White), new Piece(PieceType.Bishop, PlayerColor.White), new Piece(PieceType.Knight, PlayerColor.White), new Piece(PieceType.Rook, PlayerColor.White) },
        { new Piece(PieceType.Pawn, PlayerColor.White), new Piece(PieceType.Pawn, PlayerColor.White), new Piece(PieceType.Pawn, PlayerColor.White), new Piece(PieceType.Pawn, PlayerColor.White), new Piece(PieceType.Pawn, PlayerColor.White), new Piece(PieceType.Pawn, PlayerColor.White), new Piece(PieceType.Pawn, PlayerColor.White), new Piece(PieceType.Pawn, PlayerColor.White) },
        { Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None },
        { Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None },
        { Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None },
        { Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None, Piece.None },
        { new Piece(PieceType.Pawn, PlayerColor.Black), new Piece(PieceType.Pawn, PlayerColor.Black), new Piece(PieceType.Pawn, PlayerColor.Black), new Piece(PieceType.Pawn, PlayerColor.Black), new Piece(PieceType.Pawn, PlayerColor.Black), new Piece(PieceType.Pawn, PlayerColor.Black), new Piece(PieceType.Pawn, PlayerColor.Black), new Piece(PieceType.Pawn, PlayerColor.Black) },
        { new Piece(PieceType.Rook, PlayerColor.Black), new Piece(PieceType.Knight, PlayerColor.Black), new Piece(PieceType.Bishop, PlayerColor.Black), new Piece(PieceType.Queen, PlayerColor.Black), new Piece(PieceType.King, PlayerColor.Black), new Piece(PieceType.Bishop, PlayerColor.Black), new Piece(PieceType.Knight, PlayerColor.Black), new Piece(PieceType.Rook, PlayerColor.Black) },
    };

    public static readonly List<Position> Positions = GetPositions().ToList();

    public Board()
    {
        this.board = InitialBoard;
    }

    public Board(Board board)
    {
        this.board = board.board;
    }

    public Board(Move move, Board board)
    {
        this.board = (Piece[,])board.board.Clone();
        this[move.Destination] = move.Piece;
        this[move.Origin] = Piece.None;
    }

    public Piece this[Position position]
    {
        get
        {
            return this.board[position.Rank, position.File];
        }
        set
        {
            value.Moved = true;
            this.board[position.Rank, position.File] = value;
        }
    }

    public bool InBounds(Position position)
    {
        return position.Rank < this.board.GetLength(0)
            && position.File < this.board.GetLength(1)
            && position.Rank >= 0
            && position.File >= 0;
    }

    private static IEnumerable<Position> GetPositions()
    {
        for (int rank = 0; rank < 8; rank++)
        {
            for (int file = 0; file < 8; file++)
            {
                yield return new Position(rank, file);
            }
        }
    }
}