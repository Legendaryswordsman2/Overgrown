using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLerp : MonoBehaviour
{
	[HideInInspector] public Vector3 endPosition;
	[HideInInspector] public int damage;

	[HideInInspector] public int selectionIndex = 0;
	[HideInInspector] public PlayerUnit playerUnit;
	private void Update()
	{
		if (endPosition == null || endPosition == new Vector3()) return;

		

		transform.position = Vector2.MoveTowards(transform.position, endPosition, 15 * Time.deltaTime);

		transform.up = endPosition - transform.position;

		if (transform.position == endPosition)
		{
			float modifiedDamage = GetAttackModifier();

			bool isCritical;

			if (modifiedDamage > damage)
				isCritical = true;
			else
				isCritical = false;

			BattleSystem.instance.enemiesAlive[selectionIndex].TakeDamage((int)modifiedDamage, isCritical);
			StartCoroutine(playerUnit.NextTurn());

			GetComponent<SpriteRenderer>().enabled = false;
			endPosition = new Vector3();
			playerUnit.TestWinState();
			StartCoroutine(DestroyTimer());
		}
	}

	float GetAttackModifier()
	{
		int temp = Random.Range(0, 101);

		float modifiedDamage = damage;

		if (temp < 20)
			modifiedDamage *= 1.20f;
		else if (temp <= 30)
			modifiedDamage = 0;

		return modifiedDamage;
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
