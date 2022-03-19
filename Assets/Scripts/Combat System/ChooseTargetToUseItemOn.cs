using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTargetToUseItemOn : MonoBehaviour
{
	BattleSystem battleSystem;
	bool AwaitingTargetToUseItemOn = false;
	BaseUnit unitToUseItemOn;
	PlayerUnit playerUnit;
	PlayerPlantUnit plantUnit;
	ConsumableItem itemToUse;
	ItemSlot itemSlot;

	private void Awake()
	{
		battleSystem = BattleSystem.instance;

		playerUnit = battleSystem.playerUnit;
		plantUnit = battleSystem.playerPlantUnit;

		unitToUseItemOn = playerUnit;
	}
	private void Update()
	{
		if (AwaitingTargetToUseItemOn)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				itemToUse.UseItem(unitToUseItemOn);
				Destroy(itemSlot.gameObject);
				unitToUseItemOn.transform.GetChild(1).gameObject.SetActive(false);

				if (unitToUseItemOn == playerUnit)
					StartCoroutine(playerUnit.playerHUD.SetHealthFromItem(playerUnit.currentHealth));
				else
					StartCoroutine(plantUnit.playerPlantHUD.SetHealthFromItem(plantUnit.currentHealth));

				AwaitingTargetToUseItemOn = false;
			}

			if (Input.GetKeyDown(KeyCode.W))
			{
				unitToUseItemOn = playerUnit;
				playerUnit.transform.GetChild(1).gameObject.SetActive(true);
				plantUnit.transform.GetChild(1).gameObject.SetActive(false);
			}

			if (Input.GetKeyDown(KeyCode.S))
			{
				unitToUseItemOn = plantUnit;
				plantUnit.transform.GetChild(1).gameObject.SetActive(true);
				playerUnit.transform.GetChild(1).gameObject.SetActive(false);
			}

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				unitToUseItemOn.transform.GetChild(1).gameObject.SetActive(false);
				unitToUseItemOn = playerUnit;
				AwaitingTargetToUseItemOn = false;
			}
		}
	}
	public void ChooseTargetToUseItem(ConsumableItem _itemToUse, ItemSlot _itemSlot)
	{
		itemToUse = _itemToUse;
		itemSlot = _itemSlot;

		playerUnit.transform.GetChild(1).gameObject.SetActive(true);
		plantUnit.transform.GetChild(1).gameObject.SetActive(false);

		AwaitingTargetToUseItemOn = true;
	}
}
