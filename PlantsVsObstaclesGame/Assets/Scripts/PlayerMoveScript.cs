using System;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class PlayerMoveScript : MonoBehaviour
{
    public Action OnHop;

    private Vector3 destination;

    public Rigidbody rb;

    public float moveSpeed;

    public bool reachedDestination = true;

    private void Update()
    {
        CheckMoveInput();
    }

    private void FixedUpdate()
    {
        TryMove();
    }

    public void CheckMoveInput()
    {
        if (!reachedDestination)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 up = new Vector3(0, 0, PlaceManagerScript.gridSize);
            Hop(up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector3 down = new Vector3(0, 0, -PlaceManagerScript.gridSize);
            Hop(down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 left = new Vector3(-PlaceManagerScript.gridSize, 0, 0);
            Hop(left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 right = new Vector3(PlaceManagerScript.gridSize, 0, 0);
            Hop(right);
        }
    }

    public void Hop(Vector3 hopDirection)
    {
        // Check if there is a block ahead and try jump if on top of that block isnt covered

        destination = transform.position + hopDirection;
        reachedDestination = false;

        OnHop?.Invoke();

    }

    public void TryMove()
    {
        if (reachedDestination)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        if (CheckIfReachedDestination())
        {
            ForceSnapOnDestination();
            reachedDestination = true;
            
        }
    }

    public bool CheckIfReachedDestination()
    {
        float distance = Vector3.Distance(transform.position, destination);
        if (distance <= 0.01f)
        {
            return true;
        }

        return false;
    }

    // Called from animator;
    public void ForceSnapOnDestination()
    {
        //rb.linearVelocity = Vector3.zero;
        transform.position = destination;
    }
}
