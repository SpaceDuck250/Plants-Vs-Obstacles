using UnityEngine;
using System.Collections.Generic;
using System;

public class SelectorManager : MonoBehaviour
{
    public List<ItemData> itemsToDisplay = new List<ItemData>();

    public List<SelectorScript> selectorsList = new List<SelectorScript>();

    public Transform container;

    public SelectorScript selectorPrefab;

    public static SelectorScript currentlySelectedSelector;

    public static SelectorManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GenerateAllSelectors();
    }

    public void GenerateAllSelectors()
    {
        ClearSelectors();

        foreach (ItemData item in itemsToDisplay)
        {
            SelectorScript newSelector = Instantiate(selectorPrefab, container);

            newSelector.SetupSelf(item);

            selectorsList.Add(newSelector);
        }

        currentlySelectedSelector = null;
    }

    private void ClearSelectors()
    {
        selectorsList.Clear();

        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}

[Serializable]
public struct ItemData
{
    public ItemData(PlaceableData placeable, int startAmount)
    {
        placeableData = placeable;
        this.startAmount = startAmount;
    }

    public PlaceableData placeableData;
    public int startAmount;
}