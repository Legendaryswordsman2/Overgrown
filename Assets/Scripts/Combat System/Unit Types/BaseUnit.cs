using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum CurrentMode { Null, Attacking, ReturningHome}
public abstract class BaseUnit : MonoBehaviour
{
	[SerializeField] protected string unitName = "Unit";

	[Header("Stats")]
	public int maxHealth = 100;
	[ReadOnlyInspector] public int currentHealth;
	[SerializeField] protected int damage = 10;

	// Private
	TMP_Text damageText;
	SpriteRenderer sr;

	[HideInInspector] public bool isBlocking = false;

	protected BattleSystem battleSystem;
	[SerializeField] protected Vector3 locationToAttackTarget;
	protected Animator anim;
	protected Vector3 basePosition;


	// Lerp
	protected float elapsedTime;

	protected CurrentMode currentMode = CurrentMode.Attacking;

	// To be implemented

	//int defence;
	// int critChance

	private void Awake()
	{
		Setup();
	}
	protected virtual void Setup()
	{
		// Set References
		battleSystem = BattleSystem.instance;
		damageText = GetComponentInChildren<TMP_Text>(true);
		anim = GetComponent<Animator>();
		basePosition = transform.position;
		sr = GetComponent<SpriteRenderer>();

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

	protected bool LerpToTarget(Vector3 startPosition, Vector3 endPosition)
	{
		elapsedTime += Time.deltaTime;
		float percentageCompleted = elapsedTime / 1;

		transform.position = Vector3.Lerp(startPosition, endPosition, percentageCompleted);

		if(transform.position == endPosition)
		{
			elapsedTime = 0;
			return true;
	    }
		return false;
	}

	protected void FlipSprite()
	{
		sr.flipX = !sr.flipX;
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
