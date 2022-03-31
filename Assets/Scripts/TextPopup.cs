using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{
	[SerializeField] TMP_Text popupText;
	public void SetPopup(string text, float duration = 1)
	{
		popupText.text = text;

		gameObject.SetActive(true);

		StartCoroutine(DisableTextTimer(duration));
	}

	IEnumerator DisableTextTimer(float duration)
	{
		yield return new WaitForSecondsRealtime(duration);
		gameObject.SetActive(false);
	}
}
