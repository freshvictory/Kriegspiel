public class Player
{
    public PlayerColor Color { get; }
    
    public bool InCheck { get; set; }

    public Player(PlayerColor playerColor)
    {
        this.Color = playerColor;
        this.InCheck = false;
    }
}