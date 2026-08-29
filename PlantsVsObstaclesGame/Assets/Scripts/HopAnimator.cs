using UnityEngine;

public class HopAnimator : MonoBehaviour
{
    public PlayerMoveScript playerMove;
    public Animator playerAnimator;

    private void Start()
    {
        playerMove.OnHop += OnHop;
        playerMove.OnHopJump += OnJump;
    }

    private void OnDestroy()
    {
        playerMove.OnHop -= OnHop;
        playerMove.OnHopJump -= OnJump;

    }

    public void OnHop()
    {
        playerAnimator.SetTrigger("Hop");
    }

    private void OnJump()
    {
        // will call move function when reached peak
        playerAnimator.SetTrigger("HopJump");
    }
}
