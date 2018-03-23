public static class Legality
{
    // Most of these checks are done in the move options check,
    // but separating them here allows us to have more helpful error messaging
    public static Rule CheckMove(Move move, Board board, Player player)
    {
        if (move.Piece.Color == PlayerColor.None)
        {
            return Rule.NotAPiece;
        }

        if (move.Piece.Color != player.Color)
        {
            return Rule.OutOfTurn;
        }
        
        if (move.Delta == Delta.Zero)
        {
            return Rule.Unmoved;
        }

        if (!board.InBounds(move.Destination))
        {
            return Rule.OutOfBounds;
        }

        if (move.Piece.Color == board[move.Destination].Color)
        {
            return Rule.SquareOccupied;
        }

        return MoveOptions.GetCheckedMoveOptions(move.Origin, board).Contains(move.Destination)
            ? Rule.None
            : Rule.Impossible;
    }
}
