using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableItemManager : MonoBehaviour
{
    public static UseableItemManager instance;

    [field: SerializeField, ReadOnlyInspector] public ConsumableItem currentItemToBeUsed { get; set; }

    [Space]

    [SerializeField] GameObject itemSlotPrefab;

    [SerializeField] PlayerItem playerItem;

    [SerializeField] GameObject itemSlotsParent;

    Inventory inventory;

    bool hasBeenInitialized = false;

    private void Awake()
    {
        instance = this;

        inventory = Inventory.instance;

        gameObject.SetActive(false);
    }

    private void Start()
    {
        UseItemItemSlot itemSlot = Instantiate(itemSlotPrefab, itemSlotsParent.transform).GetComponent<UseItemItemSlot>();
        itemSlot.SetSlot(playerItem);

        SetPlantsInUseItemMenu();

        hasBeenInitialized = true;
    }

    private void OnEnable()
    {
        if (hasBeenInitialized)
        {
            RefreshUseItemMenu();
        }

    }

    public void RefreshUseItemMenu()
    {
        RemovePlantsFromUseItemMenu();
        SetPlantsInUseItemMenu();
    }
    void RemovePlantsFromUseItemMenu()
    {
        foreach (Transform child in itemSlotsParent.transform)
        {
            if (child.GetComponent<UseItemItemSlot>().item is not PlayerItem)
            Destroy(child.gameObject);
        }
    }

    void SetPlantsInUseItemMenu()
    {
        for (int i = 0; i < inventory.equippablePlantItems.Count; i++)
        {
            var ItemSlot = Instantiate(itemSlotPrefab, itemSlotsParent.transform).GetComponent<UseItemItemSlot>();
            ItemSlot.SetSlot(inventory.equippablePlantItems[i]);
            //equippablePlantItemSlots.Add(ItemSlot);
        }
    }
}
