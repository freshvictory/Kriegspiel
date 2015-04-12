using System.Collections;
using UnityEngine;

public class Legality {

    public static bool Check(string piece, Vector2 position, Vector2 move, char[,] board) {
        switch (piece) {
            case "P":
                return PawnLegality(position, move, board);
            case "K":
                return KnightLegality(move);
            case "B":
                return BishopLegality(position, move, board);
            case "R":
                return RookLegality(position, move, board);
            case "Q":
                return QueenLegality(position, move, board);
            case "*":
                return KingLegality(move);
            default:
                return false;
        }
    }

    private static bool PawnLegality(Vector2 position, Vector2 move, char[,] board) {
        bool legal = true;
        if (position.y == 1) {
            legal = Mathf.Abs(move.x) <= 1 && move.y > 0 && move.y <= 2 && board[(int)(position.y + move.y), (int)(position.x + move.x)] == ' ' 
                && board[(int)(position.y + 1), (int)(position.x + move.x)] == ' ';
        } else {
            legal = Mathf.Abs(move.x) <= 1 && move.y > 0 && move.y <= 1 && board[(int)(position.y + move.y), (int)(position.x + move.x)] == ' ';
        }
        if (move.x == 1 && move.y == 1) {
            legal = board[(int)position.y + 1, (int)position.x + 1] != ' ';
        } else if (move.x == -1 && move.y == 1) {
            legal = board[(int)position.y + 1, (int)position.x - 1] != ' ';
        }
        return legal;
    }

    private static bool KnightLegality(Vector2 move) {
        return (Mathf.Abs(move.x) == 2 && Mathf.Abs(move.y) == 1) || (Mathf.Abs(move.y) == 2 && Mathf.Abs(move.x) == 1);
    }

    private static bool BishopLegality(Vector2 position, Vector2 move, char[,] board) {
        bool legal = Mathf.Abs(move.x) == Mathf.Abs(move.y);
        float xSign = Mathf.Sign(move.x);
        float ySign = Mathf.Sign(move.y);
        for (int i = 1; i < Mathf.Abs(move.x); i++) {
            legal = legal && board[(int)(position.y + ySign * i), (int)(position.x + xSign * i)] == ' ';
        }
        return legal;
    }

    private static bool RookLegality(Vector2 position, Vector2 move, char[,] board) {
        bool legal = move.x == 0 || move.y == 0;
        float xSign = Mathf.Sign(move.x);
        float ySign = Mathf.Sign(move.y);
        for (int i = 1; i < Mathf.Abs(move.x); i++) {
            legal = legal && board[(int)(position.y), (int)(position.x + xSign * i)] == ' ';
        }
        for (int i = 1; i < Mathf.Abs(move.y); i++) {
            legal = legal && board[(int)(position.y + ySign * i), (int)(position.x)] == ' ';
        }
        return legal;
    }

    private static bool QueenLegality(Vector2 position, Vector2 move, char[,] board) {
        return BishopLegality(position, move, board) || RookLegality(position, move, board);
    }

    private static bool KingLegality(Vector2 move) {
        return move.x <= 1 && move.y <= 1;
    }
	
}
