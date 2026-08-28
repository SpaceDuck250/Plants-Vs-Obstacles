using UnityEngine;

[CreateAssetMenu(fileName = "PlaceableData", menuName = "Scriptable Objects/PlaceableData")]
public class PlaceableData : ScriptableObject
{
    public string placeableName;
    public GameObject placePrefab;

    public Direction possiblePlaceSide;
}

public enum Direction
{
    All,
    TopOnly,
    SidesOnly
}