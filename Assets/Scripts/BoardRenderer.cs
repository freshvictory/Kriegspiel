using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BoardRenderer : MonoBehaviour
{
	public GameObject Canvas;
    public Text BoardText;
    public Text LegalText;
	public Text DebugText;

	public PlayerColor Player;

	public GameObject PiecePrefab;

	[Header("Images")]
	public PieceImage Pawn;
	public PieceImage Rook;
	public PieceImage Knight;
	public PieceImage Bishop;
	public PieceImage Queen;
	public PieceImage King;

	public float Offset;

	private IDictionary<PieceType, PieceImage> pieceImages;
	private PieceObjects pieceObjects = new PieceObjects();
	
	public State State;

    void Start()
    {
	    this.pieceImages = new Dictionary<PieceType, PieceImage>
	    {
		    [PieceType.Pawn] = this.Pawn,
		    [PieceType.Rook] = this.Rook,
		    [PieceType.Knight] = this.Knight,
		    [PieceType.Bishop] = this.Bishop,
		    [PieceType.Queen] = this.Queen,
		    [PieceType.King] = this.King
	    };

	    this.Offset = this.GetComponent<RectTransform>().sizeDelta.x / 8;

	    this.MakeBoard(this.Player);
	    this.DrawBoard();
    }

	void Update()
	{
		var stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("turn: " + this.State.Turn);

		if (this.State.LastMove != null)
		{
			stringBuilder.AppendLine("last move: " + this.State.LastMove);
		}

		if (this.State.LastAttemptedMove != null)
		{
			stringBuilder.AppendLine("last attempted move: " + this.State.LastAttemptedMove);
		}

		this.DebugText.text = stringBuilder.ToString();
	}

    private void MakeBoard(PlayerColor playerColor) {
        this.State = new State(playerColor);
    }

    public void DrawBoard() {
	    var newPieceObjects = new PieceObjects();

	    foreach (var position in Board.Positions)
	    {
			var piece = this.State.Board[position];

			if (piece.Color != PlayerColor.None)
			{
				var pieceObject = this.pieceObjects.GetNext(piece);

				if (pieceObject == null)
				{
					pieceObject = this.InstantiatePiece(piece);
				}

				newPieceObjects.Add(piece, pieceObject);
				
				this.PositionPiece(pieceObject, position);
			}
		}

	    this.BoardText.text = this.State.Board.GetBoardText();

	    var takenPieces = this.pieceObjects.GetTakenPieces();
	    foreach (var takenPiece in takenPieces)
	    {
		    Destroy(takenPiece);
	    }
	    
	    this.pieceObjects = newPieceObjects;
    }

	private GameObject InstantiatePiece(Piece piece)
	{
		var pieceObject = Instantiate(this.PiecePrefab);
		
		var pieceImage = this.pieceImages[piece.Type];
		var sprite = piece.Color == PlayerColor.White ? pieceImage.White : pieceImage.Black;
		var image = pieceObject.GetComponent<Image>();
		image.sprite = sprite;

		pieceObject.name = piece.Type + " (" + piece.Color + ")";
		
		return pieceObject;

	}
	
	private void PositionPiece(GameObject pieceObject, Position position)
	{
		pieceObject.transform.SetParent(this.gameObject.transform);

		var rectTransform = pieceObject.GetComponent<RectTransform>();
		
		var positionVector = position.ToVector2(this.Offset);
		
		rectTransform.localRotation = Quaternion.identity;
		rectTransform.localScale = Vector3.one;
		rectTransform.offsetMin = positionVector;
		rectTransform.offsetMax = positionVector;
	}
}
