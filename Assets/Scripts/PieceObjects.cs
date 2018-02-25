using System;
using System.Collections.Generic;
using UnityEngine;

public class PieceObjects
{
    private IDictionary<PieceType, Queue<GameObject>> whiteObjects = new Dictionary<PieceType, Queue<GameObject>>
    {
        [PieceType.Pawn] = new Queue<GameObject>(),
        [PieceType.Rook] = new Queue<GameObject>(),
        [PieceType.Bishop] = new Queue<GameObject>(),
        [PieceType.Knight] = new Queue<GameObject>(),
        [PieceType.Queen] = new Queue<GameObject>(),
        [PieceType.King] = new Queue<GameObject>()
    };
    
    private IDictionary<PieceType, Queue<GameObject>> blackObjects = new Dictionary<PieceType, Queue<GameObject>>
    {
        [PieceType.Pawn] = new Queue<GameObject>(),
        [PieceType.Rook] = new Queue<GameObject>(),
        [PieceType.Bishop] = new Queue<GameObject>(),
        [PieceType.Knight] = new Queue<GameObject>(),
        [PieceType.Queen] = new Queue<GameObject>(),
        [PieceType.King] = new Queue<GameObject>()
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
                this.whiteObjects[piece.Type].Enqueue(gameObject);
                break;
            case PlayerColor.Black:
                this.blackObjects[piece.Type].Enqueue(gameObject);
                break;
            case PlayerColor.None:
            default:
                break;
        }
    }

    public IEnumerable<GameObject> GetTakenPieces()
    {
        foreach (var pieceTypes in this.whiteObjects)
        {
            foreach (var gameObject in pieceTypes.Value)
            {
                yield return gameObject;
            }
        }

        foreach (var pieceTypes in this.blackObjects)
        {
            foreach (var gameObject in pieceTypes.Value)
            {
                yield return gameObject;
            }
        }
    }

    private static GameObject GetPiece(IDictionary<PieceType, Queue<GameObject>> objects, PieceType type)
    {
        return objects[type].Count > 0
            ? objects[type].Dequeue()
            : null;
    }
}