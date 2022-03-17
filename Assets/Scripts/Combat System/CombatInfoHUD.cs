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
		nameText.text = unit.unitName.ToUpper();
		healthSlider.maximum = unit.maxHealth;
		healthSlider.current = unit.maxHealth;
	}
	public void SetHealth(int health)
	{
		healthSlider.current = health;
	}

	public IEnumerator SetHealthFromItem(int health)
	{
		yield return new WaitForSeconds(1.5f);
		healthSlider.current = health;
	}
}
