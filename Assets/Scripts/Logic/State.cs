using System.Collections.Generic;

public class State
{
    public PlayerColor Player { get; set; }
    
    public PlayerColor Turn { get; private set; }
    
    public Board Board { get; }
    
    public readonly IDictionary<PlayerColor, IList<Piece>> TakenPieces = new Dictionary<PlayerColor, IList<Piece>>
    {
        { PlayerColor.White, new List<Piece>() },
        { PlayerColor.Black, new List<Piece>() },
    };
    
    public Move LastMove { get; private set; }
    
    public Move LastAttemptedMove { get; private set; }

    public State()
    {
        this.Player = PlayerColor.White;
        this.Turn = PlayerColor.White;
        this.Board = new Board();
    }

    public State(PlayerColor playerColor)
    {
        this.Player = playerColor;
        this.Turn = PlayerColor.White;
        this.Board = new Board();
    }

    public State(PlayerColor turn, Board board)
    {
        this.Turn = turn;
        this.Board = board;
    }

    public MoveResult Move(Position origin, Position destination)
    {
        var move = new Move(origin, destination, this.Board);

        move.Legality = Legality.CheckMove(move, this.Board, this.Turn);

        this.LastAttemptedMove = move;

        if (move.Legality == Rule.None)
        {
            this.LastMove = this.LastAttemptedMove;
            
            this.CompleteMove(move);

            var win = false;
            if (win)
            {
                move.MoveResult = MoveResult.Checkmate;
                return MoveResult.Checkmate;
            }

            move.MoveResult = MoveResult.Legal;
        }
        else
        {
            move.MoveResult = MoveResult.Illegal;
        }

        return move.MoveResult;
    }

    private void CompleteMove(Move move)
    {
        var takenPiece = this.Board[move.Destination];

        if (takenPiece.Color != PlayerColor.None)
        {
            this.TakenPieces[this.Turn].Add(takenPiece);
        }

        this.Board[move.Destination] = move.Piece;
        this.Board[move.Origin] = Piece.None;

        this.Turn = this.Turn.Enemy();
    }
}