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

        for (int i = 0; i < inventory.equippablePlantItems.Count; i++)
        {
            var ItemSlot = Instantiate(itemSlotPrefab, itemSlotsParent.transform).GetComponent<UseItemItemSlot>();
            ItemSlot.SetSlot(inventory.equippablePlantItems[i]);
            //equippablePlantItemSlots.Add(ItemSlot);
        }
    }
}
