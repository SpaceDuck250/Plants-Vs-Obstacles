using UnityEngine;
using System;

public class RotateDraggerScript : MonoBehaviour
{
    public float mouseX;
    public float sensitivity;

    public Camera cam;

    public Transform pivotPoint;

    public bool holding = false;

    public event Action OnDraggingScreen;

    private void Update()
    {
        CheckIfHoldingMouse();
        if (!holding)
        {
            return;
        }

        mouseX = Input.GetAxis("Mouse X");

        Vector3 turnX = mouseX * sensitivity * Time.deltaTime * Vector3.up;


        pivotPoint.Rotate(turnX);
    }

    private void CheckIfHoldingMouse()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            holding = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            holding = false;
        }
    }
}
