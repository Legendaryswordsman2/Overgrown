using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	[Header("Starting Stats")]
	[SerializeField] int maxHealth = 100;
	[SerializeField] int defense = 0;
	[SerializeField] int damage = 10;
}
