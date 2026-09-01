using UnityEngine;
using System;


public class SelectorScript : MonoBehaviour
{
    public SelectorUIScript selectorUIScript;
    public PlaceableData placeableHeld;
    public int amountLeft;

    public static event Action<SelectorScript> OnSelected;

    public void SetupSelf(ItemData item)
    {
        selectorUIScript.SetupSelectorUI(item);

        placeableHeld = item.placeableData;     
        amountLeft = item.startAmount;
    }

    public void DecrementAmount()
    {

        amountLeft--;

        selectorUIScript.DecrementAmountText(amountLeft);

    }

    public void WhenMouseClick()
    {
        selectorUIScript.DoMouseClickUI();

        OnSelected?.Invoke(this);

    }


}
