using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TweenText : MonoBehaviour
{
	[SerializeField] float tweenTime;

	[SerializeField] GameObject text;

	[SerializeField] LeanTweenType easeType;

	public void Tween()
	{
		LeanTween.cancel(gameObject);

		transform.localScale = Vector3.one;

		LeanTween.scale(gameObject, Vector3.one * 1.1f, tweenTime)
			.setEase(easeType);

		StartCoroutine(ResetTimer());
	}

	IEnumerator ResetTimer()
	{
		yield return new WaitForSeconds(1);
		gameObject.transform.localScale = new Vector3(1, 1, 1);
	}

}
