using UnityEngine;
using System;

// 1 2 3 4... 2, 4, 6, 8...
public class PlacerScript : MonoBehaviour
{
    public static float gridSize = 1;

    public Camera cam;

    public float maxRange;

    public PlaceableData placeBlock;
    public LayerMask placeLayer;

    public event Action<PlaceableData> OnPlacedBlock;

    public bool canPlace = true;

    private void Update()
    {
        if (!canPlace)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(mouseRay, out hitInfo, maxRange, placeLayer))
            {
                //Vector3 hitPoint = OffsetPlacePoint(hitInfo.point, hitInfo.normal);
                PlaceInfo snappedPointInfo = SnapPointToGrid(hitInfo);

                PlaceBlock(placeBlock, snappedPointInfo);
            }
        }
    }

    private PlaceInfo SnapPointToGrid(RaycastHit hitInfo)
    {
        Vector3 normal = hitInfo.normal;

        Vector3 placePosition = hitInfo.collider.transform.position;

        float x = placePosition.x;
        float y = placePosition.y;
        float z = placePosition.z;
        // (a, b, c)
        // this one can be (a +- gridSize, b +- gridSize, c+- gridSize) since we want a valid position

        Vector3 snappedPosition = new Vector3(x + (normal.x * gridSize), y + (normal.y * gridSize), z + (normal.z * gridSize));
        Direction placeDirection = GetDirectionFromNormal(normal);


        PlaceInfo placeInfo = new PlaceInfo(snappedPosition, placeDirection);

        return placeInfo;

    }

    private void PlaceBlock(PlaceableData placeBlock, PlaceInfo placeInfo)
    {
        if (placeBlock == null)
        {
            return;
        }

        if (placeBlock.possiblePlaceSide != Direction.All && placeBlock.possiblePlaceSide != placeInfo.placeDirection)
        {
            return;
        }

        Instantiate(placeBlock.placePrefab, placeInfo.placePosition, Quaternion.identity);

        OnPlacedBlock?.Invoke(placeBlock);
    }

    public Direction GetDirectionFromNormal(Vector3 normal)
    {
        if (normal == Vector3.up || normal == -Vector3.up)
        {
            return Direction.Top;
        }
        else
        {
            return Direction.Sides;
        }
    }
}

public struct PlaceInfo
{
    public PlaceInfo(Vector3 placePos, Direction placeDir)
    {
        placePosition = placePos;
        placeDirection = placeDir;
    }

    public Vector3 placePosition;
    public Direction placeDirection;

}
