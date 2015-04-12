using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

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
    //private string player {
    //    public get { return player; }
    //    public set { player = value; }
    //}

    //private char[,] board {
    //    public get { if (player == "white") return whiteBoard; else return blackBoard; }
    //}

    //public char[,] UpdateBoard(Vector2 old, Vector2 current) {
    //    char piece = board[(int)old.x, (int)old.y];
    //    board[(int)old.x, (int)old.y] = ' ';
    //    board[(int)current.x, (int)current.y] = piece;
    //    return board;
    //}
}
