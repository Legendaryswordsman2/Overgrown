using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum CurrentMode { Null, Attacking, ReturningHome, AwaitingTargetToAttack, Dead}
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




	[HideInInspector] public CurrentMode currentMode = CurrentMode.Null;

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
			bool finished = LerpToTarget(locationToAttackTarget);

			if (finished)
			{
				OnAttack();
			}
		}

		if (currentMode == CurrentMode.ReturningHome)
		{
			bool finished = LerpToTarget(basePosition);

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
		currentMode = CurrentMode.Dead;
	}
	IEnumerator HurtWhileBlocking()
	{
		anim.Play("Hurt Animation");
		yield return new WaitForSeconds(battleSystem.backToBlockAnimationDelay);
		anim.Play("Block Animation");
	}

	public void UseItem()
	{
		anim.Play("Use Item Animation");
	}

	public virtual void Heal(int amount)
	{
		currentHealth += amount;
		UseItem();
	}

	protected bool LerpToTarget(Vector3 endPosition)
	{
		transform.position = Vector2.MoveTowards(transform.position, endPosition, battleSystem.walkSpeed * Time.deltaTime);

		if(transform.position == endPosition)
		{
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

		if(battleSystem.playerUnit.currentMode == CurrentMode.Dead)
		{
			StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", "Title"));
		}

		if(battleSystem.enemiesAlive.Count == 0)
		{
			StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", BattleSetupData.sceneIndex, BattleSetupData.playerPosition));
		}
	}

	protected virtual void OnAttack()
	{
		currentMode = CurrentMode.Null;
		anim.Play("Attack Animation");
		StartCoroutine(ReturnToBasePOS());
	}

	public virtual void ResetForNewRound()
	{
		anim.Play("Idle Animation");
		isBlocking = false;
	}

	public virtual void ChooseAction()
	{
		isBlocking = false;
	}
	public abstract IEnumerator NextTurn();

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
