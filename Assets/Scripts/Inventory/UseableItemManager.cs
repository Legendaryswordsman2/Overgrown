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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UseItemItemSlot itemSlot = Instantiate(itemSlotPrefab, itemSlotsParent.transform).GetComponent<UseItemItemSlot>();
        itemSlot.SetSlot(playerItem);
    }
}
