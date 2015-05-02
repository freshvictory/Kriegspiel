using System.Collections;
using UnityEngine;

public class Legality {

    public static bool inCheck;

    public static bool Check(GameObject piece, Vector2 position, Vector2 move, GameObject[,] board) {

        // Check that we're on the board
        bool legal = ((position.x + move.x) < 8 && (position.x + move.x) >= 0 && (position.y + move.y) < 8 && (position.y + move.y) >= 0);

        if (!legal) {
            return legal;
        }

        // Check that we're not moving onto our own piece
        if (board[(int)(position.y + move.y), (int)(position.x + move.x)] != null) {
            legal = legal && board[(int)(position.y + move.y), (int)(position.x + move.x)].layer != piece.layer;
        }

        switch (piece.tag) {
            case "P":
                legal = legal && PawnLegality(piece, position, move, board);
                break;
            case "K":
                legal = legal && KnightLegality(piece, position, move, board);
                break;
            case "B":
                legal = legal && BishopLegality(piece, position, move, board);
                break;
            case "R":
                legal = legal && RookLegality(piece, position, move, board);
                break;
            case "Q":
                legal = legal && QueenLegality(piece, position, move, board);
                break;
            case "I":
                legal = legal && KingLegality(piece, move);
                break;
            default:
                legal = false;
                break;
        }

        if (legal) {
            piece.name = piece.name.ToCharArray()[0] + "";
        }

        return legal;
    }

    private static bool PawnLegality(GameObject piece, Vector2 position, Vector2 move, GameObject[,] board) {
        bool legal = true;

        // Can we move two squares?
        if (position.y == 1) {
            legal = Mathf.Abs(move.x) <= 1 &&  move.y > 0 && move.y <= 2 && board[(int)(position.y + move.y), (int)(position.x + move.x)] == null 
                && board[(int)(position.y + 1), (int)(position.x + move.x)] == null;
        } else {
            legal = Mathf.Abs(move.x) <= 1 && move.y > 0 && move.y <= 1 && board[(int)(position.y + move.y), (int)(position.x + move.x)] == null;
        }

        // Can we move diagonally?
        if (Mathf.Abs(move.x) == 1 && move.y == 1) {
            legal = board[(int)position.y + 1, (int)(position.x + move.x)] != null;
        }
        return legal;
    }

    private static bool KnightLegality(GameObject piece, Vector2 position, Vector2 move, GameObject[,] board) {

        // Check to see if we know how to move a knight
        return (Mathf.Abs(move.x) == 2 && Mathf.Abs(move.y) == 1) || (Mathf.Abs(move.y) == 2 && Mathf.Abs(move.x) == 1);
    }

    private static bool BishopLegality(GameObject piece, Vector2 position, Vector2 move, GameObject[,] board) {

        // Check to see if we know how to move a bishop
        bool legal = Mathf.Abs(move.x) == Mathf.Abs(move.y);

        // Which way are we moving?
        float xSign = Mathf.Sign(move.x);
        float ySign = Mathf.Sign(move.y);

        // Check there was nothing in the path
        for (int i = 1; i < Mathf.Abs(move.x); i++) {
            legal = legal && board[(int)(position.y + ySign * i), (int)(position.x + xSign * i)] == null;
        }

        return legal;
    }

    private static bool RookLegality(GameObject piece, Vector2 position, Vector2 move, GameObject[,] board) {

        // Check to see we know how to move a rook
        bool legal = move.x == 0 || move.y == 0;

        // Which way are we moving?
        float xSign = Mathf.Sign(move.x);
        float ySign = Mathf.Sign(move.y);

        // Check to see there was nothing in the path
        for (int i = 1; i < Mathf.Abs(move.x); i++) {
            legal = legal && board[(int)(position.y), (int)(position.x + xSign * i)] == null;
        }
        for (int i = 1; i < Mathf.Abs(move.y); i++) {
            legal = legal && board[(int)(position.y + ySign * i), (int)(position.x)] == null;
        }

        return legal;
    }

    private static bool QueenLegality(GameObject piece, Vector2 position, Vector2 move, GameObject[,] board) {
        return BishopLegality(piece, position, move, board) || RookLegality(piece, position, move, board);
    }

    private static bool KingLegality(GameObject piece, Vector2 move) {
        return move.x <= 1 && move.y <= 1;
    }

    private static bool BishopCheck(GameObject piece, Vector2 position, GameObject[,] board) {
        bool isInCheck = false;



        return isInCheck;
    }
}
