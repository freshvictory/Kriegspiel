using System.Collections;
using UnityEngine;

public class Legality {

    public static bool Check(string piece, Vector2 move) {
        switch (piece) {
            case "Pawn":
                return PawnLegality(move);
            case "Knight":
                return KnightLegality(move);
            case "Bishop":
                return BishopLegality(move);
            case "Rook":
                return RookLegality(move);
            case "Queen":
                return QueenLegality(move);
            case "King":
                return KingLegality(move);
            default:
                return false;
        }
    }

    private static bool PawnLegality(Vector2 move) {
        Debug.Log(move);
        return move.x <= 1 && move.y > 0 && move.y <= 2;
    }

    private static bool KnightLegality(Vector2 move) {
        return (Mathf.Abs(move.x) == 2 && Mathf.Abs(move.y) == 1) || (Mathf.Abs(move.y) == 2 && Mathf.Abs(move.x) == 1);
    }

    private static bool BishopLegality(Vector2 move) {
        return Mathf.Abs(move.x) == Mathf.Abs(move.y);
    }

    private static bool RookLegality(Vector2 move) {
        return move.x == 0 || move.y == 0;
    }

    private static bool QueenLegality(Vector2 move) {
        return BishopLegality(move) || RookLegality(move);
    }

    private static bool KingLegality(Vector2 move) {
        return move.x <= 1 && move.y <= 1;
    }
	
}
