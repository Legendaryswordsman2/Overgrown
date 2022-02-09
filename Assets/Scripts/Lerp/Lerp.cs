using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Lerp
{
	static float elapsedTime;
	public static bool StartLerp(Transform gameObject, Vector3 StartPosition, Vector3 EndPosition, float duration)
	{
		elapsedTime += Time.deltaTime;
		float percentageComplete = elapsedTime / duration;

		gameObject.position = Vector3.Lerp(StartPosition, EndPosition, percentageComplete);

		if (gameObject.position == EndPosition)
		{
			return true;
		}
		return false;
	}
}
