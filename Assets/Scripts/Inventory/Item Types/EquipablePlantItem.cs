using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipable Plant Item")]
public class EquipablePlantItem : Item
{
	[Header("Unit Settings")]
	public string unitName = "Some Random Plant";
	public int defaultHealth = 100;
	public int attackDamage = 10;

	[Header("Leveling")]
	public int healthIncreasePerLevelUp = 10;
	public int damageIncreasePerLevelUp = 2;

	public RuntimeAnimatorController animatorController;



	bool isEquipped = false;
	public override void ItemSelected(ItemSlot itemSlot)
	{
		isEquipped = !isEquipped;
		Debug.Log("Plant Item Selected");
		BattleSetupData.plantSO = this;
	}
}
