using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectorUIScript : MonoBehaviour
{
    public TextMeshProUGUI placeableName;
    public Image placeableSprite;
    public TextMeshProUGUI amountText;

    public Animator selectorAnimator;

    public void SetupSelectorUI(ItemData item)
    {
        placeableName.text = item.placeableData.placeableName;

        if (item.placeableData.placeAbleSprite != null)
        {
            placeableSprite.sprite = item.placeableData.placeAbleSprite;

        }
        amountText.text = item.startAmount.ToString();
    }

    public void ResetColorForAll()
    {
        foreach (SelectorScript selector in SelectorManager.instance.selectorsList)
        {
            selector.selectorUIScript.SetPictureNormal();
        }
    }

    public void SetPictureGray()
    {
        placeableSprite.color = Color.gray;
    }

    public void SetPictureNormal()
    {
        placeableSprite.color = Color.white;
    }

    public void DoMouseClickUI()
    {
        ResetColorForAll();
        SetPictureGray();
        WhenMouseExit();
    }

    public void DecrementAmountText(int newAmount)
    {
        amountText.text = newAmount.ToString();
    }

    public void WhenMouseHover()
    {
        selectorAnimator.SetBool("Scale", true);
    }

    public void WhenMouseExit()
    {
        selectorAnimator.SetBool("Scale", false);
    }
}
