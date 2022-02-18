using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleHUD : MonoBehaviour
{
	public TMP_Text nameText;
	public TMP_Text levelText;
	public ProgressBar healthSlider;

	public void SetHUD(BaseUnit unit)
	{
		nameText.text = unit.name;
		levelText.text = "Lvl " + unit.unitLevel;
		healthSlider.maximum = unit.maxHealth;
		healthSlider.current = unit.currentHealth;
	}

	public void SetHealth(int health)
	{
		healthSlider.current = health;
	}
}