using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleText : MonoBehaviour
{
	TMP_Text titleText;

	private void Awake()
	{
		titleText = GetComponentInChildren<TMP_Text>();
	}
	public void SetTitle(string text, float duration)
	{
		titleText.text = text;

		gameObject.SetActive(true);

		StartCoroutine(DisableTextTimer(duration));
	}

	IEnumerator DisableTextTimer(float duration)
	{
		yield return new WaitForSecondsRealtime(duration);
		gameObject.SetActive(false);
	}
}
