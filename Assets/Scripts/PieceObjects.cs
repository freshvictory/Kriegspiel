using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceObjects
{
    private IDictionary<PlayerColor, IDictionary<PieceType, Queue<GameObject>>> pieceObjects;

    public PieceObjects()
    {
        this.pieceObjects = new Dictionary<PlayerColor, IDictionary<PieceType, Queue<GameObject>>>
        {
            [PlayerColor.White] = GetObjectDict(),
            [PlayerColor.Black] = GetObjectDict()
        };
    }

    private static IDictionary<PieceType, Queue<GameObject>> GetObjectDict() 
        => new Dictionary<PieceType, Queue<GameObject>>
            {
                [PieceType.Pawn] = new Queue<GameObject>(),
                [PieceType.Rook] = new Queue<GameObject>(),
                [PieceType.Bishop] = new Queue<GameObject>(),
                [PieceType.Knight] = new Queue<GameObject>(),
                [PieceType.Queen] = new Queue<GameObject>(),
                [PieceType.King] = new Queue<GameObject>()
            };

    public GameObject GetNext(Piece piece) => GetPiece(this.pieceObjects[piece.Color], piece.Type);

    public void Add(Piece piece, GameObject gameObject) => this.pieceObjects[piece.Color][piece.Type].Enqueue(gameObject);

    public IEnumerable<GameObject> GetTakenPieces() => this.pieceObjects.SelectMany(player => player.Value.SelectMany(piece => piece.Value));

    private static GameObject GetPiece(IDictionary<PieceType, Queue<GameObject>> objects, PieceType type)
    {
        return objects[type].Count > 0
            ? objects[type].Dequeue()
            : null;
    }
}