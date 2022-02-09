using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLerp : MonoBehaviour
{
	static float elapsedTime;
	float duration = 0.5f;
	Vector3 startPosition;
	[SerializeField] Transform endPosition;

	private void Start()
	{
		startPosition = transform.position;
	}
	private void Update()
	{
		elapsedTime += Time.deltaTime;
		float percentageComplete = elapsedTime / duration;

		transform.position = Vector3.Lerp(startPosition, endPosition.position, percentageComplete);

		if (transform.position == endPosition.position)
		{
			Debug.Log("Reached Position");
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(endPosition.position, 0.1f);
	}
}
