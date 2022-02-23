using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
enum AttackType { Basic, Ranged}
public class PlayerUnit : BaseUnit
{
	[field: Header("References")]
	[field: SerializeField] public CombatInfoHUD playerHUD { get; private set; }

	AttackType attackType;

	int enemySelectionIndex = 0;
	protected override void Setup()
	{
		unitName = SteamFriends.GetPersonaName();
		base.Setup();
		playerHUD.SetHUD(this);
	}

	protected override void Update()
	{
		base.Update();

		if(currentMode == CurrentMode.AwaitingTargetToAttack)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				if (attackType == AttackType.Basic)
					BasicAttack();
				else
					RangedAttack();
			}

			if (Input.GetKeyDown(KeyCode.W))
			{
				if (enemySelectionIndex <= 0) return;
				enemySelectionIndex--;

				for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
				{
					battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
				}
				battleSystem.enemiesAlive[enemySelectionIndex].transform.GetChild(1).gameObject.SetActive(true);

				Debug.Log(enemySelectionIndex);
			}

			if (Input.GetKeyDown(KeyCode.S))
			{
				if (enemySelectionIndex >= battleSystem.enemiesAlive.Count - 1) return;
			    enemySelectionIndex++;

				for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
				{
					battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
				}
				battleSystem.enemiesAlive[enemySelectionIndex].transform.GetChild(1).gameObject.SetActive(true);

				Debug.Log(enemySelectionIndex);
			}
		}
	}
	public override void TakeDamage(int _damage)
	{
		base.TakeDamage(_damage);
		if (currentHealth >= 0)
		{
			playerHUD.SetHealth(currentHealth);
		}
		else
		{
			playerHUD.SetHealth(0);
		}
	}
	protected override void Die()
	{
		base.Die();
		StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", BattleSetupData.sceneIndex));
	}
	public override void ChooseAction()
	{
		base.ChooseAction();

		if (anim != null)
			anim.Play("Idle Animation");
		battleSystem.playerChoices.SetActive(true);
	}

	#region Actions
	public void SelectTargetToAttack(bool isBasicAttack)
	{
		if (isBasicAttack)
			attackType = AttackType.Basic;
		else
			attackType = AttackType.Ranged;

		battleSystem.playerChoices.SetActive(false);

		if (enemySelectionIndex < 0) enemySelectionIndex = 0;

		if(enemySelectionIndex > battleSystem.enemiesAlive.Count - 1) enemySelectionIndex = battleSystem.enemiesAlive.Count - 1;

		battleSystem.enemiesAlive[enemySelectionIndex].transform.GetChild(1).gameObject.SetActive(true);

		currentMode = CurrentMode.AwaitingTargetToAttack;
	}
	void BasicAttack()
	{
		for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
		{
			battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
		}

		locationToAttackTarget = battleSystem.enemiesAlive[enemySelectionIndex].transform.GetChild(2).position;
		anim.Play("Walk Animation");
		currentMode = CurrentMode.Attacking;
	}
	void RangedAttack()
	{
		for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
		{
			battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
		}

		locationToAttackTarget = battleSystem.enemiesAlive[enemySelectionIndex].transform.GetChild(2).position;
		ArrowLerp arrow = Instantiate(battleSystem.arrowPrefab, this.transform).GetComponent<ArrowLerp>();
		arrow.damage = damage;
		arrow.selectionIndex = enemySelectionIndex;
		arrow.endPosition = battleSystem.enemiesAlive[enemySelectionIndex].transform.position;
	}
	public override void Block()
	{
		battleSystem.playerChoices.SetActive(false);
		base.Block();
	}
	protected override void OnAttack()
	{
		base.OnAttack();
		battleSystem.enemiesAlive[enemySelectionIndex].TakeDamage(damage);
	}
	protected override void OnReturnedToBasePosition()
	{
		base.OnReturnedToBasePosition();
		StartCoroutine(NextTurn());
	}

	protected override IEnumerator NextTurn()
	{
		yield return new WaitForSeconds(battleSystem.DelayBetweenEachTurn);
		if (battleSystem.playerHasPlant)
		{
			battleSystem.playerChoices.SetActive(false);
			battleSystem.playerPlantUnit.ChooseAction();
		}
		else
		{
			battleSystem.SwitchTurn();
		}
	}
	public void EscapeBattle()
	{
		StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", BattleSetupData.sceneIndex, BattleSetupData.playerPosition));
	}
	#endregion
}
