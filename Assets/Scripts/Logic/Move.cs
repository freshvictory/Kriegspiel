using System.Text;

public class Move
{
    public Piece Piece { get; }
    
    public Position Origin { get; }
    
    public Position Destination { get; }
    
    public Delta Delta { get; }
    
    public Rule Legality { get; set; }
    
    public MoveResult MoveResult { get; set; }

    public Move(Position origin, Position destination, Board board)
    {
        this.Origin = origin;
        this.Destination = destination;
        this.Piece = board[origin];
        this.Delta = new Delta(this.Origin, this.Destination);
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder
            .AppendLine(this.Piece.Color + " " + this.Piece.Type)
            .Append("(" + (this.Origin.Rank + 1) + ", " + (this.Origin.File + 1) + ") -> ")
            .AppendLine("(" + (this.Destination.Rank + 1) + ", " + (this.Destination.File + 1) + ")")
            .AppendLine("result: " + this.Legality);

        return stringBuilder.ToString();
    }
}