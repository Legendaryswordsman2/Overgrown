using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLerp : MonoBehaviour
{
	float elapsedTime;
	float duration = 0.5f;
	Vector3 startPosition;
	[HideInInspector] public Vector3 endPosition;
	[HideInInspector] public int damage;

	[HideInInspector] public int selectionIndex = 0;

	private void Start()
	{
		startPosition = transform.position;
	}
	private void Update()
	{
		if (endPosition == null || endPosition == new Vector3()) return;

		elapsedTime += Time.deltaTime;
		float percentageComplete = elapsedTime / duration;

		transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);

		if (transform.position == endPosition)
		{
			BattleSystem.instance.enemiesAlive[selectionIndex].TakeDamage(damage);
			Debug.Log("Switching Turn");
			StartCoroutine(BattleSystem.instance.SwitchTurn());

			GetComponent<SpriteRenderer>().enabled = false;
			endPosition = new Vector3();
			StartCoroutine(DestroyTimer());
		}
	}

	IEnumerator DestroyTimer()
	{
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(endPosition, 0.1f);
	}
}
