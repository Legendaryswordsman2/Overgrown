using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
	[SerializeField] float timeUntilDisable = 2;
	private void OnEnable()
	{
		StartCoroutine(Timer());
	}
	IEnumerator Timer()
	{
		yield return new WaitForSeconds(timeUntilDisable);
		gameObject.SetActive(false);
	}
}
