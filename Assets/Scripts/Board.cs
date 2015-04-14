using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Board : MonoBehaviour {

    public Text boardText;
    public Text legalText;
    private Vector2 min = new Vector2(0.25f, 0.05555104f);
    private float offsetX = 0.0623f;
    private float offsetY = 0.111f;
    private Vector2 max = new Vector2(0.313f, 0.1667f);

    [Header("White Prefabs")]
    public GameObject pawnW;
    public GameObject rookW;
    public GameObject knightW;
    public GameObject bishopW;
    public GameObject queenW;
    public GameObject kingW;

    [Header("Black Prefabs")]
    public GameObject pawnB;
    public GameObject rookB;
    public GameObject knightB;
    public GameObject bishopB;
    public GameObject queenB;
    public GameObject kingB;

    public GameObject blank;



    private GameObject[] row7 = new GameObject[8];
    private GameObject[] row6 = new GameObject[8];
    private GameObject[] row5 = new GameObject[8];
    private GameObject[] row4 = new GameObject[8];
    private GameObject[] row3 = new GameObject[8];
    private GameObject[] row2 = new GameObject[8];
    private GameObject[] row1 = new GameObject[8];
    private GameObject[] row0 = new GameObject[8];
    private GameObject[][] pieces;
    public GameObject[,] gameBoard;
    private GameObject canvas;

    private char[,] whiteBoard = new char[8, 8] {    {'R', 'K', 'B', 'Q', 'I', 'B', 'K', 'R'}, 
                                                     {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                                                     {'R', 'K', 'B', 'Q', 'I', 'B', 'K', 'R'}   };

    private char[,] blackBoard = new char[8, 8] {    {'R', 'K', 'B', 'I', 'Q', 'B', 'K', 'R'}, 
                                                     {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                                     {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                                                     {'R', 'K', 'B', 'I', 'Q', 'B', 'K', 'R'}   };
    public string player;
    
    public char[,] board {
        get { if (player == "white") return whiteBoard; else return blackBoard; }
        set { board = value; }
    }

    void Start() {
        canvas = GameObject.Find("Canvas");
        pieces = new GameObject[8][];
        pieces[0] = row0;
        pieces[1] = row1;
        pieces[2] = row2;
        pieces[3] = row3;
        pieces[4] = row4;
        pieces[5] = row5;
        pieces[6] = row6;
        pieces[7] = row7;
        MakeBoard(player);
        DisplayBoard();
        DrawBoard();
    }

    private void MakeBoard(string player) {
        gameBoard = new GameObject[8, 8];
        Movement movement;
        if (player == "white") {

            // Pawns
            for (int i = 0; i < 8; i++) {

                // White Pawns
                gameBoard[1, i] = Instantiate(pawnW);
                gameBoard[1, i].transform.SetParent(canvas.transform, false);
                movement = gameBoard[1, i].GetComponent<Movement>();
                movement.statusText = legalText;
                movement.board = this;

                // Black Pawns
                gameBoard[6, i] = Instantiate(pawnB);
                gameBoard[6, i].transform.SetParent(canvas.transform, false);
                movement = gameBoard[6, i].GetComponent<Movement>();
                movement.statusText = legalText;
                movement.board = this;
            }

            // White Rooks
            gameBoard[0, 0] = Instantiate(rookW);
            movement = gameBoard[0, 0].GetComponent<Movement>();
            gameBoard[0, 0].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;

            gameBoard[0, 7] = Instantiate(rookW);
            movement = gameBoard[0, 7].GetComponent<Movement>();
            gameBoard[0, 7].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;

            // Black Rooks
            gameBoard[7, 0] = Instantiate(rookB);
            movement = gameBoard[7, 0].GetComponent<Movement>();
            gameBoard[7, 0].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;

            gameBoard[7, 7] = Instantiate(rookB);
            movement = gameBoard[7, 7].GetComponent<Movement>();
            gameBoard[7, 7].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;

            // White Knights
            gameBoard[0, 1] = Instantiate(knightW);
            movement = gameBoard[0, 1].GetComponent<Movement>();
            gameBoard[0, 1].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;

            gameBoard[0, 6] = Instantiate(knightW);
            movement = gameBoard[0, 6].GetComponent<Movement>();
            gameBoard[0, 6].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;

            // Black Knights
            gameBoard[7, 1] = Instantiate(knightB);
            movement = gameBoard[7, 1].GetComponent<Movement>();
            gameBoard[7, 1].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;

            gameBoard[7, 6] = Instantiate(knightB);
            movement = gameBoard[7, 6].GetComponent<Movement>();
            gameBoard[7, 6].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;

            // White Bishops
            gameBoard[0, 2] = Instantiate(bishopW);
            movement = gameBoard[0, 2].GetComponent<Movement>();
            gameBoard[0, 2].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;

            gameBoard[0, 5] = Instantiate(bishopW);
            movement = gameBoard[0, 5].GetComponent<Movement>();
            gameBoard[0, 5].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;

            // Black Bishops
            gameBoard[7, 2] = Instantiate(bishopB);
            movement = gameBoard[7, 2].GetComponent<Movement>();
            gameBoard[7, 2].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;

            gameBoard[7, 5] = Instantiate(bishopB);
            movement = gameBoard[7, 5].GetComponent<Movement>();
            gameBoard[7, 5].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;

            // White Queen
            gameBoard[0, 3] = Instantiate(queenW);
            movement = gameBoard[0, 3].GetComponent<Movement>();
            gameBoard[0, 3].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;

            // Black Queen
            gameBoard[7, 3] = Instantiate(queenB);
            movement = gameBoard[7, 3].GetComponent<Movement>();
            gameBoard[7, 3].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;

            // White King
            gameBoard[0, 4] = Instantiate(kingW);
            movement = gameBoard[0, 4].GetComponent<Movement>();
            gameBoard[0, 4].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;

            // Black King
            gameBoard[7, 4] = Instantiate(kingB);
            movement = gameBoard[0, 4].GetComponent<Movement>();
            gameBoard[7, 4].transform.SetParent(canvas.transform, false);
            movement.statusText = legalText;
            movement.board = this;


        }
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
                if (gameBoard[i, j] == null) {
                    boardText.text += "  ";
                } else {
                    boardText.text += gameBoard[i, j].name.ToCharArray()[0] + " ";
                }
            }
            boardText.text += "\n";
        }
    }

    public void DrawBoard() {
        for (int j = 0; j < 8; j++) {
            for (int i = 0; i < 8; i++) {
                if (gameBoard[j, i] != null) {
                    RectTransform rectTransform = gameBoard[j, i].GetComponent<RectTransform>();

                    Vector2 offset = new Vector2(offsetX * i, offsetY * j);
                    rectTransform.anchorMin = new Vector2(0.25f, 0.05555104f) + offset;
                    rectTransform.anchorMax = new Vector2(0.313f, 0.1667f) + offset;
                    rectTransform.offsetMin = Vector2.zero;
                    rectTransform.offsetMax = Vector2.zero;
                }
            }
        }
    }
}
