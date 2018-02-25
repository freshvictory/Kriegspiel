using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Board : IEnumerable<Piece>
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

    public Board()
    {
        this.board = InitialBoard;
    }

    public Board(Board board)
    {
        this.board = board.board;
    }

    public Piece this[Position position]
    {
        get { return this.board[position.Rank, position.File]; }
        set
        {
            value.Moved = true;
            this.board[position.Rank, position.File] = value;
        }
    }

    public IEnumerator<Piece> GetEnumerator() => (IEnumerator<Piece>)this.board.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    
    public string GetBoardText(PlayerColor color = PlayerColor.White)
    {
        var stringBuilder = new StringBuilder();

        if (color == PlayerColor.Black)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    stringBuilder
                        .Append(this.board[i, j].Type.Display())
                        .Append(" ");
                }
                
                stringBuilder.AppendLine();
            }
        }
        else
        {
            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    stringBuilder
                        .Append(this.board[i, j].Type.Display())
                        .Append(" ");
                }
                
                stringBuilder.AppendLine();
            }
        }

        return stringBuilder.ToString();
    }
}