using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum CurrentMode { Null, Attacking, ReturningHome, AwaitingTargetToAttack, Dead}
public abstract class BaseUnit : MonoBehaviour
{
	[ReadOnlyInspector] public string unitName = "Unit";

	[Header("Stats")]
	[ReadOnlyInspector] public int maxHealth = 100;
	[ReadOnlyInspector] public int currentHealth;
	[ReadOnlyInspector] public int damage = 10;
	[ReadOnlyInspector] public int defense = 0;
	[ReadOnlyInspector] public int critChance = 0;

	// Private
	TMP_Text damageText;
	SpriteRenderer sr;

	[HideInInspector] public bool isBlocking = false;

	protected BattleSystem battleSystem;
	protected Vector3 locationToAttackTarget;
	protected Animator anim;
	protected Vector3 basePosition;




	[HideInInspector] public CurrentMode currentMode = CurrentMode.Null;
	private void Awake()
	{
		Setup();
	}

	protected virtual void Update()
	{
		if (currentMode == CurrentMode.Attacking)
		{
			bool finished = LerpToTarget(locationToAttackTarget, battleSystem.walkSpeed);

			if (finished)
			{
				OnAttack();
			}
		}

		if (currentMode == CurrentMode.ReturningHome)
		{
			bool finished = LerpToTarget(basePosition, battleSystem.walkBackToBasePositionSpeed);

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
	public virtual void TakeDamage(int _damage, bool isCritical)
	{
		if(defense != 0)
		{
			float tempDefense = 0;
			float tempDamage = _damage;

			for (int i = 0; i < defense; i++)
			{
				tempDefense += battleSystem.defensePercentagePerDefensePoint;
			}

			tempDamage = -(((tempDamage * tempDefense) / 100) - tempDamage);
			_damage = (int)tempDamage;
		}

		if(_damage <= 0)
		{
			damageText.text = "MISS";
			damageText.color = Color.red;
			damageText.gameObject.SetActive(true);
			return;
		}

		if (isCritical)
			damageText.color = Color.green;

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

		if(isCritical == false)
		damageText.color = Color.white;

		damageText.gameObject.SetActive(true);
		currentHealth -= _damage;

		if (currentHealth <= 0) Die();
	}
	protected virtual void Die()
	{
		gameObject.SetActive(false);
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
		if(currentHealth + amount > maxHealth)
		{
			currentHealth = maxHealth;
		}
		else
		{
		currentHealth += amount;
		}
	}

	protected bool LerpToTarget(Vector3 endPosition, float walkSpeed)
	{
		transform.position = Vector2.MoveTowards(transform.position, endPosition, walkSpeed * Time.deltaTime);

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
		TestWinState();
	}

	public void TestWinState()
	{
		if (battleSystem.playerUnit.currentMode == CurrentMode.Dead)
		{
			battleSystem.battleLostScreen.SetActive(true);
			battleSystem.gameOverScreen.SetActive(true);
			GameManager.StopTime();
			return;
		}

		if (battleSystem.enemiesAlive.Count == 0)
		{
			battleSystem.xpGainedText.text += " " + battleSystem.xpGiven;
			battleSystem.moneyGainedText.text += " " + battleSystem.moneyGiven;

			battleSystem.battleWonScreen.SetActive(true);
			battleSystem.gameOverScreen.SetActive(true);
			GameManager.StopTime();
		}
	}

	protected virtual void OnAttack()
	{
		currentMode = CurrentMode.Null;
		anim.Play("Attack Animation");
		StartCoroutine(ReturnToBasePOS());
	}
	protected float GetAttackModifier()
	{
		float temp = Random.Range(0, 101);

		float modifiedDamage = damage;

		float _critChance = battleSystem.baseCritChancePercantage;

		for (int i = 0; i < critChance; i++)
		{
			_critChance += battleSystem.critPercentagePerCritPoint;
		}

		if (temp < _critChance)
			modifiedDamage *= 1.20f;
		else if (temp <= _critChance + 10)
			modifiedDamage = 0;

		return modifiedDamage;
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
