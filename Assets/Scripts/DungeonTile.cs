using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTile : ScriptableObject
{
    [SerializeField] GameObject piece;
    [SerializeField] GameObject cornerPiece;
    [SerializeField] GameObject tPiece;
    [SerializeField] GameObject crossPiece;

    public enum TileType {floor, wall };
    public TileType tileTyle;
}
