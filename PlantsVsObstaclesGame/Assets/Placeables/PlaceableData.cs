using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlaceableData", menuName = "Scriptable Objects/PlaceableData")]
public class PlaceableData : ScriptableObject
{
    public string placeableName;
    public GameObject placePrefab;

    public Direction possiblePlaceSide;

    public bool canPlaceOnEverything = true;

    public List<PlaceableData> blocksCanPlaceOn = new List<PlaceableData>();
}

public enum Direction
{
    All,
    Top,
    Sides
}