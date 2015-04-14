using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Movement : MonoBehaviour,  IDragHandler, IEndDragHandler {

    public Board board;
    public Text statusText;
    private Vector2 min = new Vector2(0.25f, 0.05555104f);
    private float offsetX = 0.0623f;
    private float offsetY = 0.111f;
    private Vector2 max = new Vector2(0.313f, 0.1667f);
    private bool initial = true;
    private Vector3 initialPosition;

	// Use this for initialization
	void Start () {
        // Add an EventTrigger to watch for dragging
        //EventTrigger e = gameObject.AddComponent<EventTrigger>();
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.Drag;
        //entry.callback = new EventTrigger.TriggerEvent();
        //entry.callback.AddListener(new UnityAction<BaseEventData>(MovePiece));
        //e.delegates = new List<EventTrigger.Entry>(2);
        //e.delegates.Add(entry);
        //entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.Drop;
        //entry.callback = new EventTrigger.TriggerEvent();
        //entry.callback.AddListener(new UnityAction<BaseEventData>(SetPiece));
        //e.delegates.Add(entry);
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void MovePiece() {
        statusText.text = "Status: ";
        if (initial) {
            initialPosition = transform.position;
            initial = false;
        }
        transform.SetAsLastSibling();
        transform.position = Input.mousePosition;
        //Debug.Log("starting couroutine");
        //StartCoroutine(FindSquare());
        //Debug.Log("finished coroutine");
        //GetComponent<Image>().color = Color.blue;
    }

    public void OnDrag(PointerEventData eventData) {
        MovePiece();
    }

    public void OnEndDrag(PointerEventData eventData) {
        SetPiece();
    }

    public void SetPiece() {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float x = Mathf.Round(((transform.position.x - 215.19f) / 50));
        Debug.Log(x);
        float y =  Mathf.Round(((transform.position.y - 47.9f) / 50));
        Debug.Log(y);
        float oldX = Mathf.Round(((initialPosition.x - 215.19f) / 50));
        float oldY = Mathf.Round(((initialPosition.y - 47.9f) / 50));
        float diffX = x - oldX;
        float diffY = y - oldY;
        bool status = Legality.Check(gameObject, new Vector2(oldX, oldY), new Vector2(diffX, diffY), board.gameBoard);
        if (!status) {
            x = Mathf.Round(((initialPosition.x - 215.19f) / 50f));
            y = Mathf.Round(((initialPosition.y - 47.9f) / 50));
        }
        board.board[(int)oldY, (int)oldX] = ' ';
        board.board[(int)y, (int)x] = (char)gameObject.name.ToCharArray()[0];
        board.gameBoard[(int)oldY, (int)oldX] = null;
        if (board.gameBoard[(int)y, (int)x] != null) {
            Destroy(board.gameBoard[(int)y, (int)x]);
        }
        board.gameBoard[(int)y, (int)x] = gameObject;
        //Vector2 offset = new Vector2(offsetX * x, offsetY * y);
        //rectTransform.anchorMin = new Vector2(0.25f, 0.05555104f) + offset;
        //rectTransform.anchorMax = new Vector2(0.313f, 0.1667f) + offset;
        //rectTransform.offsetMin = Vector2.zero;
        //rectTransform.offsetMax = Vector2.zero;
        statusText.text = "Status: " + (status ?  "<color=green> Legal </color>" : "<color=red> Illegal </color>");
        board.DisplayBoard();
        board.DrawBoard();
        initial = true;
    }
}
