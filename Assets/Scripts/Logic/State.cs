public class State
{
    public PlayerColor Turn { get; set; }
    
    public Board Board { get; set; }
    
    public Move LastMove { get; private set; }
    
    public Move LastAttemptedMove { get; private set; }

    public State()
    {
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
        var piece = this.Board[origin];
        
        this.LastAttemptedMove = new Move
        {
            Piece = piece,
            Origin = origin,
            Destination = destination,
            MoveResult = MoveResult.Illegal
        };

        if (piece.Color == PlayerColor.None)
        {
            return MoveResult.Illegal;
        }
        
        if (piece.Color != this.Turn)
        {
            return MoveResult.Illegal;
        }
        
        if (!Legality.CheckMove(origin, destination, this.Board))
        {
            return MoveResult.Illegal;
        }
        
        this.Board[destination] = piece;
        this.Board[origin] = Piece.None;

        var win = false;
        if (win)
        {
            this.LastAttemptedMove.MoveResult = MoveResult.Checkmate;
            return MoveResult.Checkmate;
        }
        
        this.Turn = piece.Color == PlayerColor.White
            ? PlayerColor.Black
            : PlayerColor.White;

        this.LastAttemptedMove.MoveResult = MoveResult.Legal;
        this.LastMove = this.LastAttemptedMove;
        
        return MoveResult.Legal;
    }
}