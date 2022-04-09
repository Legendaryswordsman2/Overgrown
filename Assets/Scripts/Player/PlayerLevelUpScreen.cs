using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLevelUpScreen : MonoBehaviour
{
	[Header("Player Stats")]
	[SerializeField] TMP_Text healthTextStat;
	[SerializeField] TMP_Text damageTextStat;
	[SerializeField] TMP_Text defenseTextStat;
	[SerializeField] TMP_Text critChanceTextStat;

	[Header("Increase Stats")]
	[SerializeField] TMP_Text healthTextStatIncrease;
	[SerializeField] TMP_Text damageTextStatIncrease;
	[SerializeField] TMP_Text defenseTextStatIncrease;
	[SerializeField] TMP_Text critChanceTextStatIncrease;

	Vector3 healthStatIncreaseDefaultPOS;
	Vector3 damageStatIncreaseDefaultPOS;
	Vector3 defenseStatIncreaseDefaultPOS;
	Vector3 critChanceStatIncreaseDefaultPOS;

	private void Awake()
	{
		healthStatIncreaseDefaultPOS = healthTextStatIncrease.transform.position;
		damageStatIncreaseDefaultPOS = damageTextStatIncrease.transform.position;
		defenseStatIncreaseDefaultPOS = defenseTextStatIncrease.transform.position;
		critChanceStatIncreaseDefaultPOS = critChanceTextStatIncrease.transform.position;
	}

	public void MergePlayerStats()
	{
		LeanTween.moveX(healthTextStatIncrease.gameObject, 1075, 0.1f);
	}
	public void ResetLevelUpScreen()
	{
		healthTextStatIncrease.transform.position = healthStatIncreaseDefaultPOS;
	}
}
