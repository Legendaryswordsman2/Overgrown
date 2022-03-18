using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour
{
	[SerializeField] string animationName = "Vault One Animation";

	[SerializeField] VaultStep[] vaultSteps;

	[SerializeField] float EditorIconSize = 0.1f;


	Transform player;
	Player playerScript;
	Animator anim;
	GameManager gameManager;

	Vector3 endPosition;
	Vector3 startPosition;
	int i;
	float elapsedTime;

	bool canVault = false;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerScript = player.GetComponent<Player>();
		anim = player.GetChild(4).GetComponent<Animator>();
		gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
				StartCoroutine(StartVault());
		}
	}

	IEnumerator StartVault()
	{
		//vaultIcon.SetActive(false);
		playerScript.canWalk = false;
		anim.Play(animationName);
		for (i = 0; i < vaultSteps.Length; i++)
		  {
			player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

			Vector3 newPlayerPosition = new Vector3(vaultSteps[i].vaultStep.x, vaultSteps[i].vaultStep.y, 0);
			endPosition = player.position + newPlayerPosition;
			startPosition = player.position;
			elapsedTime = 0;

			canVault = true;

			yield return new WaitForSeconds(vaultSteps[i].duration);
		  }
		playerScript.canWalk = true;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
	}
	private void Update()
	{
		if (canVault)
		{
			elapsedTime += Time.deltaTime;
			float percentageComplete = elapsedTime / vaultSteps[i].duration;

			player.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);

			if(player.position == endPosition)
			{
				canVault = false;
			}
		}
	}
	private void OnDrawGizmosSelected()
	{
		Vector2 originSpot = new Vector2(this.transform.position.x, this.transform.position.y);

		Gizmos.DrawWireSphere(originSpot, EditorIconSize);
		Vector2[] toDraw = new Vector2[vaultSteps.Length];
		for (int i = 0; i < vaultSteps.Length; i++)
		{
			if(i >= 1)
			{
				toDraw[i] = toDraw[i - 1];
				toDraw[i] += new Vector2(vaultSteps[i].vaultStep.x, vaultSteps[i].vaultStep.y);
			}
			else
			{
				toDraw[i] += new Vector2(vaultSteps[i].vaultStep.x + this.transform.position.x, vaultSteps[i].vaultStep.y + this.transform.position.y);
			}
			Gizmos.DrawWireSphere(toDraw[i], EditorIconSize);
		}
	}
}
