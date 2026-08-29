using UnityEngine;

public class HopAnimator : MonoBehaviour
{
    public PlayerMoveScript playerMove;
    public Animator playerAnimator;

    private void Start()
    {
        playerMove.OnHop += OnHop;
    }

    private void OnDestroy()
    {
        playerMove.OnHop -= OnHop;
    }

    public void OnHop()
    {
        playerAnimator.SetTrigger("Hop");
    }
}
