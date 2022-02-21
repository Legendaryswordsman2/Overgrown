using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : BaseUnit
{
	 [field: Header("Refernces")]
	 [field: SerializeField] public SOEnemy enemySO { get; set; }

	ProgressBar healthBar;

	public void SetupEnemy()
	{
		unitName = enemySO.enemyName;
		maxHealth = enemySO.defaultHealth;
		damage = enemySO.attackDamage;
		gameObject.GetComponent<Animator>().runtimeAnimatorController = enemySO.animatorController;

		healthBar = transform.GetChild(0).GetChild(0).GetComponent<ProgressBar>();

		healthBar.maximum = maxHealth;
		healthBar.current = maxHealth;

		gameObject.SetActive(true);
	}

	public override void ChooseAction()
	{
		BasicAttack();
	}

	void BasicAttack()
	{

	}

	protected override void OnValidate()
	{
		if (enemySO == null)
		{
			gameObject.SetActive(false);
			name = "Unit";
		} 
		else
		{
			gameObject.SetActive(true);
			SetupEnemy();
			base.OnValidate();
		}
	}
}
