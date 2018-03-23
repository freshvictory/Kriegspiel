using System.Collections.Generic;
using System.Linq;

public static class Checker
{
    public static bool IsInCheck(PlayerColor color, Board board, List<Position> enemyMoveOptions)
    {
        var kingPosition = Board.Positions
            .Single(position => board[position].Color == color && board[position].Type == PieceType.King);
        
        return enemyMoveOptions.Contains(kingPosition);
    }
}