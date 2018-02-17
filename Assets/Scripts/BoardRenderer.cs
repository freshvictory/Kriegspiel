using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoardRenderer : MonoBehaviour {

    public Text boardText;
    public Text legalText;
    private Vector2 min = new Vector2(0.25f, 0.05555104f);
    private float offsetX = 0.0623f;
    private float offsetY = 0.111f;
    private Vector2 max = new Vector2(0.313f, 0.1667f);

    [Header("Prefabs")]
    public PiecePrefab Pawn;
    public PiecePrefab Rook;
    public PiecePrefab Knight;
    public PiecePrefab Bishop;
    public PiecePrefab Queen;
    public PiecePrefab King;

	private IDictionary piecePrefabs;
    
    private GameObject[][] pieces;
    public Piece[,] gameBoard;
    private GameObject canvas;

    public PlayerColor Player;

    void Start() {
        
    }

    private void MakeBoard(string player) {
        var board = new Board();
    }

	public void instantiatePiece(int x, int y, GameObject prefab) {
		GameObject piece = Instantiate(prefab);
		gameBoard[x, y] = piece.GetComponent<Piece>();
		Movement movement = piece.GetComponent<Movement>();
		piece.transform.SetParent(canvas.transform, false);
		movement.statusText = legalText;
		movement.board = this;
	}

    public void DrawBoard() {
        
    }
}
