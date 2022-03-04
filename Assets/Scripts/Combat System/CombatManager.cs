using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
	public static CombatManager instance { get; private set; }

	CombatInventory inventory;

    private void Awake()
    {
        instance = this;
		inventory = BattleSystem.instance.inventory;
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && BattleSystem.instance.state == BattleState.PlayerTurn)
		{
			BattleSystem battleSystem = BattleSystem.instance;

			BattleSystem.instance.playerChoices.SetActive(true);
			inventory.gameObject.SetActive(false);

			battleSystem.playerUnit.currentMode = CurrentMode.Null;

			for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
			{
				battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
			}
		}
	}
}
