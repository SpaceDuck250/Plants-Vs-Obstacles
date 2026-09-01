using UnityEngine;

public class PlaceManager : MonoBehaviour
{
    public PlacerScript placer;
    public SelectorScript selectorUsed;

    private void Start()
    {
        SelectorScript.OnSelected += OnSelectedNewPlaceable;
        placer.OnPlacedBlock += OnPlacedBlock;
    }

    private void OnDestroy()
    {
        SelectorScript.OnSelected -= OnSelectedNewPlaceable;
        placer.OnPlacedBlock -= OnPlacedBlock;
    }

    private void OnSelectedNewPlaceable(SelectorScript selector)
    {
        placer.placeBlock = selector.placeableHeld;
        selectorUsed = selector;

        placer.canPlace = CheckIfAnyLeft(selector) ? true : false;
    }

    private void OnPlacedBlock(PlaceableData placeableData)
    {
        selectorUsed.DecrementAmount();
        
        placer.canPlace = CheckIfAnyLeft(selectorUsed) ? true : false;
    }

    public bool CheckIfAnyLeft(SelectorScript selector)
    {
        if (selectorUsed.amountLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
