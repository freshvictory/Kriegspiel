using System.Linq;
using System.Runtime.CompilerServices;

public static class Legality
{
    public static Rule CheckMove(Move move, Board board, PlayerColor turn)
    {
        if (move.Piece.Color == PlayerColor.None)
        {
            return Rule.NotAPiece;
        }

        if (move.Piece.Color != turn)
        {
            return Rule.OutOfTurn;
        }
        
        if (move.Delta == Delta.Zero)
        {
            return Rule.Unmoved;
        }

        if (!InBounds(move.Destination))
        {
            return Rule.OutOfBounds;
        }

        if (move.Piece.Color == board[move.Destination].Color)
        {
            return Rule.SquareOccupied;
        }

        switch (move.Piece.Type)
        {
            case PieceType.Pawn:
                return PawnLegality(move, board);
            case PieceType.Knight:
                return KnightLegality(move);
            case PieceType.Bishop:
                return BishopLegality(move, board);
            case PieceType.Rook:
                return RookLegality(move, board);
            case PieceType.Queen:
                return QueenLegality(move, board);
            case PieceType.King:
                return KingLegality(move, board);
            case PieceType.None:
            default:
                return Rule.Impossible;
        }
    }

    private static bool InBounds(Position destination)
    {
        return destination.Rank <= 8
            && destination.File <= 8
            && destination.Rank >= 0
            && destination.File >= 0;
    }

    private static Rule PawnLegality(Move move, Board board)
    {
        if (move.Piece.Color == PlayerColor.White && move.Delta.Rank.Direction != (int)Sign.Positive)
        {
            return Rule.Impossible;
        }

        if (move.Piece.Color == PlayerColor.Black && move.Delta.Rank.Direction != (int)Sign.Negative)
        {
            return Rule.Impossible;
        }

        var moveLegality = PawnMoveLegality(move, board);

        return moveLegality != Rule.None
            ? PawnAttackLegality(move, board)
            : Rule.None;
    }

    // TODO: En passant
    private static Rule PawnMoveLegality(Move move, Board board)
    {
        if (move.Delta.File.Direction != (int)Sign.None)
        {
            return Rule.Impossible;
        }

        if (board[move.Destination].Color != PlayerColor.None)
        {
            return Rule.SquareOccupied;
        }

        switch (move.Delta.Rank.Magnitude)
        {
            case 1:
                return Rule.None;
            case 2:
                return move.Piece.Moved
                    ? Rule.FirstMoveOnly
                    : CheckPathCollisions(move, board);
            default:
                return Rule.Impossible;
        }
    }

    private static Rule PawnAttackLegality(Move move, Board board)
    {
        if (move.Delta == new Delta(1, 1) && board[move.Destination].Color == move.Piece.Color.Enemy())
        {
            return Rule.None;
        }

        return Rule.Impossible;
    }

    private static Rule KnightLegality(Move move)
    {
        if (move.Delta == new Delta(2, 1) || move.Delta == new Delta(1, 2))
        {
            return Rule.None;
        }

        return Rule.Impossible;
    }

    private static Rule BishopLegality(Move move, Board board)
    {
        if (!Delta.IsDiagonal(move.Delta))
        {
            return Rule.Impossible;
        }

        return CheckPathCollisions(move, board);
    }

    private static Rule RookLegality(Move move, Board board)
    {
        if (!Delta.IsHorizontal(move.Delta) && !Delta.IsVertical(move.Delta))
        {
            return Rule.Impossible;
        }

        return CheckPathCollisions(move, board);
    }

    private static Rule QueenLegality(Move move, Board board)
    {
        var bishopLegality = BishopLegality(move, board);

        return bishopLegality == Rule.Impossible
            ? RookLegality(move, board)
            : bishopLegality;
    }

    private static Rule KingLegality(Move move, Board board)
    {
        return move.Delta.Rank.Magnitude <=1 && move.Delta.File.Magnitude <= 1
            ? Rule.None
            : CastleLegality(move, board);
    }

    // TODO
    private static Rule CastleLegality(Move move, Board board)
    {
        return Rule.Impossible;
    }

    // TODO
    private static Rule CheckPathCollisions(Move move, Board board)
    {
        return Position.GetRange(move.Origin, move.Delta).Any(position => board[position].Color != PlayerColor.None)
            ? Rule.PathOccupied
            : Rule.None;
    }
}
