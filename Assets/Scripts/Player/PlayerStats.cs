using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	[Header("Starting Stats")]
	public int maxHealth = 100;
	public int defense = 0;
	public int damage = 10;
	public int critChance = 0;

	private void Awake()
	{
		
	}
}
