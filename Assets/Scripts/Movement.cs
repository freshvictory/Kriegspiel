using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour, IDragHandler, IEndDragHandler
{
	private RectTransform rectTransform;
    private BoardRenderer boardRenderer;
    private Text statusText;
	
    private bool initial = true;
    private Position originalPosition;
	private List<Position> possibleMoves;

	void Start ()
	{
		this.boardRenderer = this.GetComponentInParent<BoardRenderer>();
		this.rectTransform = this.GetComponent<RectTransform>();
		this.statusText = this.boardRenderer.LegalText;
	}

    public void OnDrag(PointerEventData eventData)
    {
	    if (this.initial)
	    {
		    this.ShowMoves();

		    this.initial = false;
	    }
	    
        this.MovePiece();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.SetPiece(this.originalPosition);
    }

	private void MovePiece()
	{
		this.rectTransform.SetAsLastSibling();
		this.rectTransform.position = Input.mousePosition;
	}

    private void SetPiece(Position origin)
    {
        var destination = this.rectTransform.offsetMax.ToPosition(this.boardRenderer.Offset);

	    var playerColor = this.boardRenderer.Player;
	    var normalizedDestination = Position.Normalize(destination, playerColor);
	    
	    var moveResult = this.boardRenderer.State.Move(origin, normalizedDestination);

        this.statusText.text = (moveResult == MoveResult.Legal 
	                               ? "<color=green> Legal </color>"
	                               : "<color=red>" + moveResult + ": " + this.boardRenderer.State.LastAttemptedMove.Legality + " </color>");
	    
        this.boardRenderer.DrawBoard();
	    
        this.boardRenderer.RemoveHighlight();
	    this.initial = true;
    }

	private void ShowMoves()
	{
		var origin = this.rectTransform.offsetMax.ToPosition(this.boardRenderer.Offset);

		var playerColor = this.boardRenderer.Player;
		this.originalPosition = Position.Normalize(origin, playerColor);
		
		this.possibleMoves = MoveOptions.GetCheckedMoveOptions(this.originalPosition, this.boardRenderer.State.Board);
		
		this.boardRenderer.HighlightPossibleMoves(this.possibleMoves);
			
		this.statusText.text = string.Join(",", this.possibleMoves);
	}
}
