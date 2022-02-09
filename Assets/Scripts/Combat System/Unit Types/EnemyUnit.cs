using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyUnit : BaseUnit
{

	[Header("Reference")]
	public SOEnemy enemySO;

	[SerializeField] Transform locationToAttackPlayer;

	ProgressBar healthBar;

	Vector3 BasePosition;

	bool isAttacking = false;
	bool isReturnigToBasePOS = false;


	// Lerping
	float elapsedTime;
	BattleSystem battleSystem;

	protected override void Awake()
	{
		base.Awake();
		BasePosition = transform.position;
		battleSystem = BattleSystem.instance;
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		if (isAttacking)
		{
			elapsedTime += Time.deltaTime;
			float percentageComplete = elapsedTime / battleSystem.walkSpeed;

			transform.position = Vector3.Lerp(BasePosition, locationToAttackPlayer.position, percentageComplete);

			if(transform.position == locationToAttackPlayer.position)
			{
				isAttacking = false;
				anim.Play("Attack Animation");
				BattleSystem.instance.playerUnit.TakeDamage(damage);
				StartCoroutine(ReturnToBasePOS());
			}
		}

		if (isReturnigToBasePOS)
		{
			elapsedTime += Time.deltaTime;
			float percentageComplete = elapsedTime / battleSystem.walkSpeed;

			transform.position = Vector3.Lerp(locationToAttackPlayer.position, BasePosition, percentageComplete);

			if (transform.position == BasePosition)
			{
				isReturnigToBasePOS = false;
				GetComponent<SpriteRenderer>().flipX = false;
				anim.Play("Idle Animation");
				BattleSystem battleSystem = BattleSystem.instance;
				StartCoroutine(NextTurn(0));
			}
		}
	}

	public void Setup()
	{
		if (enemySO == null) return;

		unitName = enemySO.enemyName;
		maxHealth = enemySO.defaultHealth;
		damage = enemySO.attackDamage;
		defaultSprite = enemySO.sprite;
		gameObject.GetComponent<Animator>().runtimeAnimatorController = enemySO.animatorController;

		healthBar = transform.GetChild(0).GetChild(0).GetComponent<ProgressBar>();

		healthBar.maximum = maxHealth;
		healthBar.current = maxHealth;

		gameObject.SetActive(true);
	}
	public override void ChooseAction()
	{
		base.ChooseAction();

		int halfOfHealth = maxHealth / 2;

		if (currentHealth >= halfOfHealth)
		{
			HealthyAI();
		}
		else if (currentHealth <= 30)
		{
			AboutToDieAI();
		}
		else if (currentHealth <= halfOfHealth)
		{
			WoundedAI();
		}
	}

	void HealthyAI()
	{
		Debug.Log(unitName + " is Healthy");
		int temp = Random.Range(0, 7);
		if (temp == 0)
		{
			Block();
		}
		else if (temp >= 1)
		{
			BasicAttack();
		}
	}
	void WoundedAI()
	{
		Debug.Log(unitName + " is Wounded");
		int temp = Random.Range(0, 4);
		if (temp == 0)
		{
			BasicAttack();
		}
		else if (temp >= 1)
		{
			Block();
		}
	}
	void AboutToDieAI()
	{
		Debug.Log( unitName + " is About To Die");
		int temp = Random.Range(0, 8);
		if (temp == 0)
		{
			BasicAttack();
		}
		else if (temp >= 1)
		{
			Block();
		}
	}

	void BasicAttack()
	{
		elapsedTime = 0;
		anim.Play("Walk Animation");
		isAttacking = true;
		//Debug.Log(unitName + " Attacked");
	}
	public override void Block()
	{
		base.Block();
		StartCoroutine(NextTurn(1));
	}
	IEnumerator ReturnToBasePOS()
	{
		elapsedTime = 0;
		yield return new WaitForSeconds(1);
		GetComponent<SpriteRenderer>().flipX = true;
		isReturnigToBasePOS = true;
		anim.Play("Walk Animation");
	}

	public override void TakeDamage(int damage)
	{
		base.TakeDamage(damage);
		healthBar.current = currentHealth;
	}

	protected override void Die()
	{
		base.Die();
		BattleSystem.instance.enemiesAlive.Remove(this);
	}

	public void ResetForNewRound()
	{
		anim.Play("Idle Animation");
		isBlocking = false;
	}

	IEnumerator NextTurn(float delay)
	{
		yield return new WaitForSeconds(delay);
		// If this unit is the last enemy to choose an action then go to player turn
		if (battleSystem.enemiesAlive[battleSystem.enemiesAlive.Count - 1] == this)
		{
			battleSystem.PlayerTurn();
		}
		else // If not then go to the next enemy
		{
			for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
			{
				if (battleSystem.enemiesAlive[i] == this)
				{
					battleSystem.enemiesAlive[i + 1].ChooseAction();
				}
			}
		}
	}

	protected override void OnValidate()
	{
		if(enemySO == null)
		{
			gameObject.SetActive(false);
			name = "Unit";
		}
		else
		{
		gameObject.SetActive(true);
		Setup();
		base.OnValidate();
		}
	}
}
