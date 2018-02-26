using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour,  IDragHandler, IEndDragHandler
{
	private RectTransform rectTransform;
    private BoardRenderer boardRenderer;
    private Text statusText;
	
    private bool initial = true;
    private Position originalPosition;

	void Start ()
	{
		this.boardRenderer = this.GetComponentInParent<BoardRenderer>();
		this.rectTransform = this.GetComponent<RectTransform>();
		this.statusText = this.boardRenderer.LegalText;
	}

    public void OnDrag(PointerEventData eventData)
    {
        this.MovePiece();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.SetPiece(this.originalPosition);
    }

	private void MovePiece()
	{
		this.statusText.text = "Status: ";
		if (this.initial)
		{
			this.originalPosition = this.rectTransform.offsetMax.ToPosition(this.boardRenderer.Offset);
			
			this.initial = false;
		}
	    
		this.rectTransform.SetAsLastSibling();
		this.rectTransform.position = Input.mousePosition;
	}

    private void SetPiece(Position origin)
    {
        var destination = this.rectTransform.offsetMax.ToPosition(this.boardRenderer.Offset);

	    var moveResult = this.boardRenderer.State.Move(origin, destination);

        this.statusText.text = (moveResult == MoveResult.Legal 
	                               ? "<color=green> Legal </color>"
	                               : "<color=red>" + moveResult + ": " + this.boardRenderer.State.LastAttemptedMove.Legality + " </color>");
	    
        this.boardRenderer.DrawBoard();
	    
        this.initial = true;
    }
}
