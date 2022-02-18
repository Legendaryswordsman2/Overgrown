using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class BaseUnit : MonoBehaviour
{
	[SerializeField] string unitName = "Unit";

	[Space]

	[Header("Stats")]
	[SerializeField] int maxHealth;
	[SerializeField, ReadOnlyInspector] int currentHealth;
	[SerializeField] int damage;

	// Private
	BattleSystem battleSystem;
	[HideInInspector] public bool isBlocking = false;

	protected Animator anim;
	TMP_Text damageText;

	// To be implemented

	//int defence;
	// int critChance

	private void Awake()
	{
		// Set Refernces
		battleSystem = BattleSystem.instance;
		damageText = GetComponentInChildren<TMP_Text>(true);
		anim = GetComponent<Animator>();

		// Set current health
		currentHealth = maxHealth;
	}
	public virtual void TakeDamage(int _damage)
	{
		if (isBlocking)
		{
			_damage /= 2;
			StartCoroutine(HurtWhileBlocking());
		}
		else
		{
			anim.Play("Hurt Animation");
		}

		damageText.text = damage.ToString();
		damageText.gameObject.SetActive(true);

		currentHealth -= damage;

		if (currentHealth <= 0) Die();
	}
	protected virtual void Die()
	{
		gameObject.SetActive(false);
		Debug.Log(unitName + " has died");
	}
	IEnumerator HurtWhileBlocking()
	{
		anim.Play("Hurt Animation");
		yield return new WaitForSeconds(battleSystem.backToBlockAnimationDelay);
		anim.Play("Block Animation");
	}

	public abstract void ChooseAction();

	#region Actions
	public virtual void Block()
	{
		isBlocking = true;
		anim.Play("Block Animation");
	}
	#endregion

	private void OnValidate()
	{
		gameObject.name = unitName.Trim() + " Unit";
	}
}
