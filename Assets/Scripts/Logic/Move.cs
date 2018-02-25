using System.Text;

public class Move
{
    public Piece Piece { get; set; }
    
    public Position Origin { get; set; }
    
    public Position Destination { get; set; }
    
    public MoveResult MoveResult { get; set; }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine(this.Piece.Color.ToString() + this.Piece.Type);
        stringBuilder.AppendLine("from (" + (this.Origin.Rank + 1) + ", " + (this.Origin.File + 1) + ")");
        stringBuilder.AppendLine("to (" + (this.Destination.Rank + 1) + ", " + (this.Destination.File + 1) + ")");
        stringBuilder.AppendLine("result: " + this.MoveResult);

        return stringBuilder.ToString();
    }
}