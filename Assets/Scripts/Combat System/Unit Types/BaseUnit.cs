using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum CurrentMode { Null, Attacking, ReturningHome, AwaitingTargetToAttack}
public abstract class BaseUnit : MonoBehaviour
{
	public string unitName = "Unit";

	[Header("Stats")]
	public int maxHealth = 100;
	[ReadOnlyInspector] public int currentHealth;
	[SerializeField] protected int damage = 10;

	// Private
	TMP_Text damageText;
	SpriteRenderer sr;

	[HideInInspector] public bool isBlocking = false;

	protected BattleSystem battleSystem;
	protected Vector3 locationToAttackTarget;
	protected Animator anim;
	protected Vector3 basePosition;


	// Lerp
	protected float elapsedTime;

	protected CurrentMode currentMode = CurrentMode.Null;

	// To be implemented

	//int defence;
	// int critChance

	private void Awake()
	{
		Setup();
	}

	protected virtual void Update()
	{
		if (currentMode == CurrentMode.Attacking)
		{
			bool finished = LerpToTarget(basePosition, locationToAttackTarget);

			if (finished)
			{
				OnAttack();
			}
		}

		if (currentMode == CurrentMode.ReturningHome)
		{
			bool finished = LerpToTarget(locationToAttackTarget, basePosition);

			if (finished)
			{
				OnReturnedToBasePosition();
			}
		}
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
		float percentageCompleted = elapsedTime / battleSystem.WalkDuration;

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

	IEnumerator ReturnToBasePOS()
	{
		yield return new WaitForSeconds(battleSystem.AttackDuration);
		FlipSprite();
		currentMode = CurrentMode.ReturningHome;
		anim.Play("Walk Animation");
	}

	protected virtual void OnReturnedToBasePosition()
	{
		currentMode = CurrentMode.Null;
		FlipSprite();
		anim.Play("Idle Animation");
	}

	protected virtual void OnAttack()
	{
		currentMode = CurrentMode.Null;
		anim.Play("Attack Animation");
		StartCoroutine(ReturnToBasePOS());
	}

	public virtual void ChooseAction()
	{
		isBlocking = false;
	}
	protected abstract IEnumerator NextTurn();

	#region Actions
	public virtual void Block()
	{
		isBlocking = true;
		anim.Play("Block Animation");
		StartCoroutine(NextTurn());
	}
	#endregion

	protected virtual void OnValidate()
	{
		gameObject.name = unitName.Trim() + " Unit";
	}
}
