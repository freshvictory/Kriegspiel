using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Board : MonoBehaviour {

    public Text boardText;
    private Vector2 min = new Vector2(0.25f, 0.05555104f);
    private float offsetX = 0.0623f;
    private float offsetY = 0.111f;
    private Vector2 max = new Vector2(0.313f, 0.1667f);
    public 

    private char[,] whiteBoard = new char[8, 8] {    {'R', 'K', 'B', 'Q', '*', 'B', 'K', 'R'}, 
                                                     {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                                                     {'R', 'K', 'B', 'Q', '*', 'B', 'K', 'R'}   };

    private char[,] blackBoard = new char[8, 8] {    {'R', 'K', 'B', '*', 'Q', 'B', 'K', 'R'}, 
                                                     {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                                                     {'R', 'K', 'B', '*', 'Q', 'B', 'K', 'R'}   };
    public string player;
    
    public char[,] board {
        get { if (player == "white") return whiteBoard; else return blackBoard; }
        set { board = value; }
    }

    void Start() {
        DisplayBoard();
    }

    public char[,] UpdateBoard(Vector2 old, Vector2 current) {
        char piece = board[(int)old.x, (int)old.y];
        board[(int)old.x, (int)old.y] = ' ';
        board[(int)current.x, (int)current.y] = piece;
        DisplayBoard();
        return board;
    }

    public void DisplayBoard() {
        boardText.text = "";
        for (int i = 7; i >= 0; i--) {
            for (int j = 0; j < 8; j++) {
                boardText.text += board[i, j] + " ";
            }
            boardText.text += "\n";
        }
    }

    public void DrawBoard() {

    }
}
