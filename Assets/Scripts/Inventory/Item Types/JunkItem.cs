using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Items/Junk Item")]
public class JunkItem : Item 
{
	public override void ItemSelected(ItemSlot itemSlot)
	{
		throw new System.NotImplementedException();
	}
}
