using UnityEngine;

public enum EquipmentType
{
	Weapon,
	Armor,
}

[CreateAssetMenu(menuName = "Items/Gear Item")]
public class GearItem : Item
{
	public EquipmentType EquipmentType;
	public override void ItemSelected(ItemSlot itemSlot)
	{
		throw new System.NotImplementedException();
	}
}
