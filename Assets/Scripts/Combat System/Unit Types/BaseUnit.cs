using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class BaseUnit : MonoBehaviour
{
	[SerializeField] string unitName = "Unit";

	[Header("Stats")]
	public int maxHealth = 100;
	[ReadOnlyInspector] public int currentHealth;
	[SerializeField] int damage = 10;

	// Private
	BattleSystem battleSystem;
	[HideInInspector] public bool isBlocking = false;

	protected Animator anim;
	TMP_Text damageText;

	// To be implemented

	//int defence;
	// int critChance

	protected virtual void Awake()
	{
		// Set Refernces
		battleSystem = BattleSystem.instance;
		damageText = GetComponentInChildren<TMP_Text>(true);
		anim = GetComponent<Animator>();

		// Set current health
		currentHealth = maxHealth;

		Setup();
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

		damageText.text = _damage.ToString();
		damageText.gameObject.SetActive(true);

		currentHealth -= _damage;

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
	protected abstract void Setup();

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
