using UnityEngine;

// 1 2 3 4... 2, 4, 6, 8...
public class PlaceManagerScript : MonoBehaviour
{
    public float gridSize = 1;

    public Camera cam;

    public float maxRange;

    public PlaceableData placeBlock;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(mouseRay, out hitInfo, maxRange))
            {
                Vector3 hitPoint = OffsetPlacePoint(hitInfo.point, mouseRay.direction);

            }
        }
    }

    private Vector3 OffsetPlacePoint(Vector3 placePoint, Vector3 rayDirection)
    {
        Vector3 oppositeVector = -rayDirection.normalized * 0.001f;

        Vector3 correctPlacePoint = placePoint + oppositeVector;

        return correctPlacePoint;
    }

    //private Vector3 SnapPointToGrid(Vector3 placePoint)
    //{
    //    // 5.9

    //}

    private void PlaceBlock(PlaceableData placeBlock, Vector3 placePosition)
    {

    }
}
