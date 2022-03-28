using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IEquippable
{
	bool isEquipped { get; set; }
	void Equip();
	void Unequip();
}
