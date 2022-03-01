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
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			BattleSystem.instance.playerChoices.SetActive(true);
			inventory.gameObject.SetActive(false);
		}
	}

	public void Do(Action action)
	{
		action.Invoke();
	}
}
