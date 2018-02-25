using System;
using System.Collections.Generic;
using UnityEngine;

public class PieceObjects
{
    private IDictionary<PieceType, Stack<GameObject>> whiteObjects = new Dictionary<PieceType, Stack<GameObject>>
    {
        [PieceType.Pawn] = new Stack<GameObject>(),
        [PieceType.Rook] = new Stack<GameObject>(),
        [PieceType.Bishop] = new Stack<GameObject>(),
        [PieceType.Knight] = new Stack<GameObject>(),
        [PieceType.Queen] = new Stack<GameObject>(),
        [PieceType.King] = new Stack<GameObject>()
    };
    
    private IDictionary<PieceType, Stack<GameObject>> blackObjects = new Dictionary<PieceType, Stack<GameObject>>
    {
        [PieceType.Pawn] = new Stack<GameObject>(),
        [PieceType.Rook] = new Stack<GameObject>(),
        [PieceType.Bishop] = new Stack<GameObject>(),
        [PieceType.Knight] = new Stack<GameObject>(),
        [PieceType.Queen] = new Stack<GameObject>(),
        [PieceType.King] = new Stack<GameObject>()
    };

    public GameObject GetNext(Piece piece)
    {
        switch (piece.Color)
        {
            case PlayerColor.White:
                return GetPiece(this.whiteObjects, piece.Type);
            case PlayerColor.Black:
                return GetPiece(this.blackObjects, piece.Type);
            case PlayerColor.None:
            default:
                return null;
        }
    }

    public void Add(Piece piece, GameObject gameObject)
    {
        switch (piece.Color)
        {
            case PlayerColor.White:
                this.whiteObjects[piece.Type].Push(gameObject);
                break;
            case PlayerColor.Black:
                this.blackObjects[piece.Type].Push(gameObject);
                break;
            case PlayerColor.None:
            default:
                break;
        }
    }

    private static GameObject GetPiece(IDictionary<PieceType, Stack<GameObject>> objects, PieceType type)
    {
        return objects[type].Count > 0
            ? objects[type].Pop()
            : null;
    }
}