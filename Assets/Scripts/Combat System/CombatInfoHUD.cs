using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatInfoHUD : MonoBehaviour
{
	[SerializeField] TMP_Text nameText;
	[SerializeField] ProgressBar healthSlider;

	public void SetHUD(BaseUnit unit)
	{
		nameText.text = unit.name;
		healthSlider.maximum = unit.maxHealth;
		healthSlider.current = unit.currentHealth;
	}
	public void SetHealth(int health)
	{
		healthSlider.current = health;
	}
}
