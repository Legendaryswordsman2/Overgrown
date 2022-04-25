using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{
	[SerializeField] TMP_Text popupText;
	public void SetPopup(string text, float duration = 1, bool pauseTime = false, Color textColor = new Color())
	{
		if (pauseTime) GameManager.StopTime();

		if(textColor == new Color())
		{
			textColor = Color.white;
			popupText.color = textColor;
		}
		else
		{
			popupText.color = textColor;
		}

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
