using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Board
{
    private readonly PlayerColor playerColor;
    
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
        this.playerColor = PlayerColor.White;
        this.board = InitialBoard;
    }

    public Board(PlayerColor playerColor)
    {
        this.playerColor = playerColor;
        this.board = InitialBoard;
    }

    public Board(PlayerColor playerColor, Board board)
    {
        this.playerColor = playerColor;
        this.board = board.board;
    }

    public Piece this[Position position]
    {
        get
        {
            var normalized = this.Normalize(position);
            return this.board[normalized.Rank, normalized.File];
        }
        set
        {
            value.Moved = true;
            var normalized = this.Normalize(position);
            this.board[normalized.Rank, normalized.File] = value;
        }
    }
    
    public string GetBoardText()
    {
        var stringBuilder = new StringBuilder();

        //        if (this.playerColor == PlayerColor.Black)
        //        {
        //            for (int i = 0; i < 8; i++)
        //            {
        //                for (int j = 0; j < 8; j++)
        //                {
        //                    stringBuilder
        //                        .Append(this.board[i, j].Type.Display())
        //                        .Append(" ");
        //                }
        //                
        //                stringBuilder.AppendLine();
        //            }
        //        }
        //        else
        //        {
        foreach (var position in Positions)
        {
            stringBuilder
                .Append(this[position].Type.Display())
                .Append(" ");

            if (position.File == 7)
            {
                stringBuilder.AppendLine();
            }
        }
        //        }

        return stringBuilder.ToString();
    }

    public Delta Vector(PlayerColor pieceColor, Position origin, Position destination)
    {
        origin = this.Normalize(origin, pieceColor);
        destination = this.Normalize(destination, pieceColor);
        
        var difference = destination - origin;
        
        return new Delta(difference.Rank, difference.File);
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

    private Position Normalize(Position position)
    {
        if (this.playerColor == PlayerColor.Black)
        {
            position = new Position(7, 7) - position;
        }

        return position;
    }
    
    private Position Normalize(Position position, PlayerColor color)
    {
        if (color == this.playerColor.Enemy())
        {
            position = new Position(7, 7) - position;
        }

        return position;
    }
}