public static class Legality
{
    public static bool CheckMove(Position origin, Position destination, Board board)
    {
        var piece = board[origin];
        var delta = Position.Delta(origin, destination);

        if (delta == Delta.Zero)
        {
            return false;
        }

        if (!BoardLegality(destination, board))
        {
            return false;
        }

        if (piece.Color == board[destination].Color)
        {
            return false;
        }

        switch (piece.Type)
        {
            case PieceType.Pawn:
                return PawnLegality(piece, origin, destination, board);
            case PieceType.Knight:
                return KnightLegality(origin, destination);
            case PieceType.Bishop:
                return BishopLegality(origin, destination, board);
            case PieceType.Rook:
                return RookLegality(origin, destination, board);
            case PieceType.Queen:
                return QueenLegality(origin, destination, board);
            case PieceType.King:
                return KingLegality(piece, origin, destination, board);
            case PieceType.None:
            default:
                return false;
        }
    }

    private static bool BoardLegality(Position destination, Board board)
    {
        return destination.Rank <= 8
            && destination.File <= 8
            && destination.Rank >= 0
            && destination.File >= 0;
    }

    private static bool PawnLegality(Piece piece, Position origin, Position destination, Board board)
    {
        var delta = Position.Delta(origin, destination);
            
        return IsForward(piece, origin, destination)
               && (PawnMoveLegality(delta, piece, origin, destination, board)
                   || PawnAttackLegality(delta, piece, destination, board));
    }

    // TODO: En passant
    private static bool PawnMoveLegality(Delta delta, Piece piece, Position origin, Position destination, Board board)
    {
        return delta.File == 0
               && board[destination].Color == PlayerColor.None
               && (delta.Rank == 1
                   || (delta.Rank == 2 && !piece.Moved));
    }

    private static bool PawnAttackLegality(Delta delta, Piece piece, Position destination, Board board)
    {
        return delta == new Delta(1, 1)
            && board[destination].Color == piece.Color.Enemy();
    }

    private static bool KnightLegality(Position origin, Position destination)
    {
        var delta = Position.Delta(origin, destination);
        
        return delta == new Delta(2, 1) || delta == new Delta(1, 2);
    }

    private static bool BishopLegality(Position origin, Position destination, Board board)
    {
        var delta = Position.Delta(origin, destination);
        
        return Delta.IsDiagonal(delta)
            && CheckPathCollisions(origin, destination, board);
    }

    private static bool RookLegality(Position origin, Position destination, Board board)
    {
        var delta = Position.Delta(origin, destination);
        
        return (Delta.IsHorizontal(delta) || Delta.IsVertical(delta))
            && CheckPathCollisions(origin, destination, board);
    }

    private static bool QueenLegality(Position origin, Position destination, Board board)
    {
        return BishopLegality(origin, destination, board) || RookLegality(origin, destination, board);
    }

    private static bool KingLegality(Piece piece, Position origin, Position destination, Board board)
    {
        return Position.Delta(origin, destination) <= new Delta(1, 1)
            || CastleLegality(piece, origin, destination, board);
    }

    // TODO
    private static bool CastleLegality(Piece piece, Position origin, Position destination, Board board)
    {
        return false;
    }

    // TODO
    private static bool CheckPathCollisions(Position origin, Position destination, Board board)
    {
        return true;
    }

    private static bool IsForward(Piece piece, Position origin, Position destination)
    {
        return Position.Normalize(destination, piece.Color).Rank > Position.Normalize(origin, piece.Color).Rank;
    }
}
