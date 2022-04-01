using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{
	[SerializeField] TMP_Text popupText;
	public void SetPopup(string text, float duration = 1, bool pauseTime = false)
	{
		if (pauseTime) GameManager.StopTime();

		popupText.text = text;

		gameObject.SetActive(true);

		StartCoroutine(DisableTextTimer(duration, pauseTime));
	}

	IEnumerator DisableTextTimer(float duration, bool pauseTime)
	{
		yield return new WaitForSecondsRealtime(duration);
		if (pauseTime) GameManager.StartTime();
		gameObject.SetActive(false);
	}
}
