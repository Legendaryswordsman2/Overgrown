using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumable Item")]
public class ConsumableItem : Item
{
	public List<ConsumableItemEffect> effects;

	public void ExecuteEffect()
	{
		//foreach (ConsumableItemEffect effect in effects)
		//{
		//	effect.ExecuteEffect(unit);
		//}
	}
}
