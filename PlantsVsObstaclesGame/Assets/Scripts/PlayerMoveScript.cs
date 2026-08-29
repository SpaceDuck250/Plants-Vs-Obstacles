using System;
using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.LightAnchor;

public class PlayerMoveScript : MonoBehaviour
{
    public event Action OnHop;
    public event Action OnHopJump;

    private Vector3 destination;

    public Rigidbody rb;

    public float moveSpeed;

    public float moveCooldownTime;

    private bool reachedDestination = true;
    private bool moveOnCooldown = false;

    public Transform playerModel;

    public Transform camParent;

    public LayerMask placeLayer;

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
        if (!reachedDestination || moveOnCooldown)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Vector3 up = new Vector3(0, 0, PlaceManagerScript.gridSize);
            Vector3 up = GiveRoundedVector(camParent.forward) * PlaceManagerScript.gridSize;

            Hop(up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Vector3 down = new Vector3(0, 0, -PlaceManagerScript.gridSize);
            Vector3 down = -GiveRoundedVector(camParent.forward) * PlaceManagerScript.gridSize;

            Hop(down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 left = -GiveRoundedVector(camParent.right) * PlaceManagerScript.gridSize;

            Hop(left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Vector3 right = new Vector3(PlaceManagerScript.gridSize, 0, 0);
            Vector3 right = GiveRoundedVector(camParent.right) * PlaceManagerScript.gridSize;

            Hop(right);
        }
    }

    public void Hop(Vector3 hopDirection)
    {
        if (CheckIfBlockAhead(hopDirection))
        {
            return;
        }

        StartCoroutine(DoMoveCooldown());
        // Check if there is a block ahead and try jump if on top of that block isnt covered

        destination = transform.position + hopDirection;
        SetupMoveToDestination();

        OnHop?.Invoke();
    }

    public bool CheckIfBlockAhead(Vector3 hopDirection)
    {
        float checkDistance = 1;
        Ray newRay = new Ray(playerModel.position, hopDirection);

        RaycastHit hitInfo;
        if (Physics.Raycast(newRay, out hitInfo, checkDistance, placeLayer))
        {
            if (CheckIfThereIsBlockAbove(hitInfo.collider.transform.position) || CheckIfThereIsBlockAbove(playerModel.transform.position))
            {
                return true;
            }

            rb.AddForce(Vector3.up * 6f, ForceMode.Impulse);
            destination = hitInfo.collider.transform.position + (Vector3.up * PlaceManagerScript.gridSize);
            Invoke("SetupMoveToDestination", 0.45f);
            //OnHopJump?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckIfThereIsBlockAbove(Vector3 blockPos)
    {
        float checkDistance = 1;
        Ray newRay = new Ray(blockPos, Vector3.up);

        if (Physics.Raycast(newRay, checkDistance, placeLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetupMoveToDestination()
    {
        rb.linearVelocity = Vector3.zero;
        reachedDestination = false;
        rb.useGravity = false;
        
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
        rb.useGravity = true;


    }

    public IEnumerator DoMoveCooldown()
    {
        moveOnCooldown = true;
        yield return new WaitForSeconds(moveCooldownTime);
        moveOnCooldown = false;
    }

    // Yes I know y isnt importance
    public static Vector3 GiveRoundedVector(Vector3 vector)
    {
        int x = Mathf.RoundToInt(vector.x);
        int y = Mathf.RoundToInt(vector.y);
        int z = Mathf.RoundToInt(vector.z);

        return new Vector3(x, y, z);
    }

}
