using System.Collections.Generic;
using System.Linq;

public static class MoveOptions
{
    private static readonly IDictionary<PieceType, List<MoveRule>> MoveRules = new Dictionary<PieceType, List<MoveRule>>
    {
        [PieceType.Rook] = new List<MoveRule>
        {
            new MoveRule(new Delta(1, 0), 8),
            new MoveRule(new Delta(0, 1), 8),
        },
        [PieceType.Bishop] = new List<MoveRule>
        {
            new MoveRule(new Delta(1, 1), 8),
        },
        [PieceType.Knight] = new List<MoveRule>
        {
            new MoveRule(new Delta(2, 1), 1),
            new MoveRule(new Delta(1, 2), 1),
        },
        [PieceType.King] = new List<MoveRule>
        {
            new MoveRule(new Delta(1, 0), 1),
            new MoveRule(new Delta(0, 1), 1),
            new MoveRule(new Delta(1, 1), 1),
        },
        [PieceType.Queen] = new List<MoveRule>
        {
            new MoveRule(new Delta(1, 0), 8),
            new MoveRule(new Delta(0, 1), 8),
            new MoveRule(new Delta(1, 1), 8),
        }
    };

    public static List<Position> GetUncheckedMoveOptions(PlayerColor color, Board board, List<Position> enemyMoveOptions)
    {
        return Board.Positions
            .Where(position => board[position].Color == color)
            .SelectMany(position => GetUncheckedMoveOptions(position, board))
            .ToList();
    }
    
    public static List<Position> GetUncheckedMoveOptions(Position origin, Board board)
    {
        var piece = board[origin];
        
        var positions = new List<Position>();
        
        switch (piece.Type)
        {
            case PieceType.Knight:
            case PieceType.Bishop:
            case PieceType.Rook:
            case PieceType.Queen:
                positions = GetPositions(piece.Color, board, origin, MoveRules[piece.Type]);
                break;
            case PieceType.King:
                positions = GetKingPositions(piece, board, origin);
                break;
            case PieceType.Pawn:
                positions = GetPawnPositions(piece, board, origin);
                break;
            case PieceType.None:
            default:
                break;
        }

        return positions;
    }

    public static List<Position> GetCheckedMoveOptions(Position origin, Board board)
    {
        var piece = board[origin];
        
        return GetUncheckedMoveOptions(origin, board)
            .Where(p => !IsInCheck(piece.Color, board, origin, p))
            .ToList();
    }

    // TODO: En passant
    private static List<Position> GetPawnPositions(Piece piece, Board board, Position origin)
    {
        var positions = new List<Position>();

        var forwardDirection = piece.Color == PlayerColor.Black ? -1 : 1;

        var forwardMove = origin + new Delta(forwardDirection, 0);

        if (PawnMoveLegality(forwardMove, board))
        {
            positions.Add(forwardMove);
        }

        if (!piece.Moved)
        {
            forwardMove = forwardMove + new Delta(forwardDirection, 0);

            if (PawnMoveLegality(forwardMove, board))
            {
                positions.Add(forwardMove);
            }
        }

        var leftCapture = origin + new Delta(forwardDirection, 1);

        if (PawnAttackLegality(leftCapture, piece.Color, board))
        {
            positions.Add(leftCapture);
        }

        var rightCapture = origin + new Delta(forwardDirection, -1);
        
        if (PawnAttackLegality(rightCapture, piece.Color, board))
        {
            positions.Add(rightCapture);
        }
        
        return positions;
    }

    private static bool PawnMoveLegality(Position destination, Board board)
    {
        return board.InBounds(destination) && board[destination].Color == PlayerColor.None;
    }

    private static bool PawnAttackLegality(Position destination, PlayerColor color, Board board)
    {
        return board.InBounds(destination) && board[destination].Color == color.Enemy();
    }

    // TODO: Castle, Check
    private static List<Position> GetKingPositions(Piece piece, Board board, Position origin)
    {
        return GetPositions(piece.Color, board, origin, MoveRules[PieceType.King]);
    }
    
    private static List<Position> GetPositions(PlayerColor color, Board board, Position origin, IEnumerable<MoveRule> moveRules)
    {
        return moveRules
            .SelectMany(moveRule => GetPositions(color, board, origin, moveRule))
            .ToList();
    }

    private static IEnumerable<Position> GetPositions(
        PlayerColor color,
        Board board,
        Position origin,
        MoveRule moveRule)
    {
        foreach (var delta in moveRule.Deltas)
        {
            var count = 0;
            var position = origin + delta;
            var pathFree = true;

            while (count++ < moveRule.MaxMultiplier && BasicLegality(color, position, board) && pathFree)
            {
                yield return position;

                pathFree = board[position].Color == PlayerColor.None;

                position += delta;
            }
        }
    }

    private static bool BasicLegality(PlayerColor color, Position position, Board board)
    {
        return board.InBounds(position) && board[position].Color != color;
    }

    private static bool IsInCheck(PlayerColor color, Board board, Position origin, Position destination)
    {
        var move = new Move(origin, destination, board);
        
        var newBoard = new Board(move, board);

        var enemyMoveOptions = GetUncheckedMoveOptions(color.Enemy(), newBoard, new List<Position>());

        return Checker.IsInCheck(color, newBoard, enemyMoveOptions);
    }
}