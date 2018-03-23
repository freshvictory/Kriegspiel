using System.Collections.Generic;

public class State
{
    private readonly Player blackPlayer = new Player(PlayerColor.Black);
    
    private readonly Player whitePlayer = new Player(PlayerColor.White);
    
    public Player Turn { get; private set; }
    
    public Board Board { get; }
    
    public readonly IDictionary<PlayerColor, IList<Piece>> TakenPieces = new Dictionary<PlayerColor, IList<Piece>>
    {
        { PlayerColor.White, new List<Piece>() },
        { PlayerColor.Black, new List<Piece>() },
    };
    
    public Move LastMove { get; private set; }
    
    public Move LastAttemptedMove { get; private set; }

    public List<Position> EnemyMoveOptions { get; set; }
    
    public List<Position> CurrentMoveOptions { get; set; }

    public State()
    {
        this.Turn = this.whitePlayer;
        this.Board = new Board();
        this.EnemyMoveOptions = new List<Position>();
    }

    public MoveResult Move(Position origin, Position destination)
    {
        var move = new Move(origin, destination, this.Board);

        move.Legality = Legality.CheckMove(move, this.Board, this.Turn);

        this.LastAttemptedMove = move;

        switch (move.Legality)
        {
            case Rule.None:
                this.LastMove = this.LastAttemptedMove;
            
                this.CompleteMove(move);

                var win = false;
                if (win)
                {
                    move.MoveResult = MoveResult.Checkmate;
                    return MoveResult.Checkmate;
                }

                move.MoveResult = MoveResult.Legal;
                break;
            case Rule.InCheck:
                move.MoveResult = MoveResult.Check;
                break;
            default:
                move.MoveResult = MoveResult.Illegal;
                break;
        }

        return move.MoveResult;
    }

    private void CompleteMove(Move move)
    {
        var takenPiece = this.Board[move.Destination];

        if (takenPiece.Color != PlayerColor.None)
        {
            this.TakenPieces[this.Turn.Color].Add(takenPiece);
        }

        this.Board[move.Destination] = move.Piece;
        this.Board[move.Origin] = Piece.None;

        this.Turn = this.Turn.Color == PlayerColor.Black ? this.whitePlayer : this.blackPlayer;

        this.EnemyMoveOptions = MoveOptions.GetUncheckedMoveOptions(this.Turn.Color.Enemy(), this.Board, this.EnemyMoveOptions);

        this.Turn.InCheck = Checker.IsInCheck(this.Turn.Color, this.Board, this.EnemyMoveOptions);
    }
}