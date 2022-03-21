using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
	public static CombatManager instance { get; private set; }

	Inventory inventory;

    private void Awake()
    {
        instance = this;
		inventory = BattleSystem.instance.inventory;
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			BattleSystem battleSystem = BattleSystem.instance;

			if (battleSystem.state == BattleState.PlayerTurn)
			{
			battleSystem.playerChoices.SetActive(true);
			battleSystem.playerUnit.currentMode = CurrentMode.Null;
			}
			else if(battleSystem.state == BattleState.PlayerPlantTurn)
			{
				battleSystem.playerPlantChoices.SetActive(true);
				battleSystem.playerPlantUnit.currentMode = CurrentMode.Null;
			}

			inventory.gameObject.SetActive(false);


			for (int i = 0; i < battleSystem.enemiesAlive.Count; i++)
			{
				battleSystem.enemiesAlive[i].transform.GetChild(1).gameObject.SetActive(false);
			}
		}
	}
}
