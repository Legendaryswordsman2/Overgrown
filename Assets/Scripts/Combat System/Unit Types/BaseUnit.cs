using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class BaseUnit : MonoBehaviour
{
	[SerializeField] protected string unitName = "Unit";

	[Header("Stats")]
	public int maxHealth = 100;
	[ReadOnlyInspector] public int currentHealth;
	[SerializeField] protected int damage = 10;

	// Private
	BattleSystem battleSystem;
	protected Transform locationtoAttackTarget;

	[HideInInspector] public bool isBlocking = false;

	protected Animator anim;
	TMP_Text damageText;

	// To be implemented

	//int defence;
	// int critChance

	private void Awake()
	{
		Setup();
	}
	protected virtual void Setup()
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

	#region Actions
	public virtual void Block()
	{
		isBlocking = true;
		anim.Play("Block Animation");
	}
	#endregion

	protected virtual void OnValidate()
	{
		gameObject.name = unitName.Trim() + " Unit";
	}
}
