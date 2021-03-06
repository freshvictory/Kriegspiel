﻿using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BoardRenderer : MonoBehaviour
{
    public Text BoardText;
    public Text LegalText;
	public Text DebugText;

	public PlayerColor Player;

	public GameObject PiecePrefab;
	public GameObject HighlightedSquarePrefab;

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
	private List<GameObject> highlightedSquareObjects;
	
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
	}

    private void MakeBoard(PlayerColor playerColor) {
        this.State = new State();
    }

    public void DrawBoard() {
	    var newPieceObjects = new PieceObjects();

	    foreach (var position in Board.Positions)
	    {
		    var normalized = Position.Normalize(position, this.Player);
		    
			var piece = this.State.Board[normalized];

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

	    var takenPieces = this.pieceObjects.GetTakenPieces();
	    foreach (var takenPiece in takenPieces)
	    {
		    Destroy(takenPiece);
	    }
	    
	    this.pieceObjects = newPieceObjects;
	    
	    this.DebugText.text = this.MoveInfo();
	    this.BoardText.text = this.TakenPieces();
    }

	public void HighlightPossibleMoves(List<Position> possibleMoves)
	{
		this.highlightedSquareObjects = new List<GameObject>();
		
		foreach (var position in possibleMoves)
		{
			var normalized = Position.Normalize(position, this.Player);

			var highlightedSquareObject = Instantiate(this.HighlightedSquarePrefab);
			
			this.highlightedSquareObjects.Add(highlightedSquareObject);
			
			this.PositionPiece(highlightedSquareObject, normalized);
		}
	}

	public void RemoveHighlight()
	{
		foreach (var highlightedSquareObject in this.highlightedSquareObjects)
		{
			Destroy(highlightedSquareObject);
		}

		this.highlightedSquareObjects = new List<GameObject>();
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

	private string MoveInfo()
	{
		var stringBuilder = new StringBuilder();
		stringBuilder
			.AppendLine("turn: <b>" + this.State.Turn.Color + "</b>")
			.AppendLine("player in check: " + this.State.Turn.InCheck)
			.AppendLine();

		if (this.State.LastMove != null)
		{
			stringBuilder
				.AppendLine("last: " + this.State.LastMove)
				.AppendLine();
		}

		if (this.State.LastAttemptedMove != null)
		{
			stringBuilder.AppendLine("tried: " + this.State.LastAttemptedMove);
		}

		return stringBuilder.ToString();
	}

	private string TakenPieces()
	{
		var stringBuilder = new StringBuilder();
		foreach (var player in this.State.TakenPieces)
		{
			stringBuilder.Append(player.Key)
				.Append(": ");
		    
			foreach (var piece in player.Value)
			{
				stringBuilder.Append(piece.Type.Display())
					.Append(" ");
			}

			stringBuilder.AppendLine();
		}

		return stringBuilder.ToString();
	}
}
