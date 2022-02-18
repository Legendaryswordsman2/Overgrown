using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLerp : MonoBehaviour
{
	float elapsedTime;
	float duration = 0.5f;
	Vector3 startPosition;
	[HideInInspector]
	public Transform endPosition;
	[HideInInspector]
	public int damage;

	private void Start()
	{
		startPosition = transform.position;
	}
	private void Update()
	{
		if (endPosition == null) return;

		elapsedTime += Time.deltaTime;
		float percentageComplete = elapsedTime / duration;

		transform.position = Vector3.Lerp(startPosition, endPosition.position, percentageComplete);

		if (transform.position == endPosition.position)
		{
			//BattleSystem.instance.enemiesAlive[BattleSystem.instance.enemySelectionIndex].TakeDamage(damage);
			//StartCoroutine(BattleSystem.instance.EnemyTurn());
			//Debug.Log("Reached Position");
			GetComponent<SpriteRenderer>().enabled = false;
			endPosition = null;
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
		Gizmos.DrawWireSphere(endPosition.position, 0.1f);
	}
}
