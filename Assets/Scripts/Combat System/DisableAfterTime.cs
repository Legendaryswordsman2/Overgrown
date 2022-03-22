using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
	[SerializeField] float timeUntilDisable = 2;
	private void OnEnable()
	{
		StartCoroutine(Timer());
	}
	IEnumerator Timer()
	{
		yield return new WaitForSecondsRealtime(timeUntilDisable);
		gameObject.SetActive(false);
	}
}
