using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChooseTargetToUseItemOn : MonoBehaviour
{
	BattleSystem battleSystem;
	bool AwaitingTargetToUseItemOn = false;
	BaseUnit unitToUseItemOn;
	PlayerUnit playerUnit;
	PlayerPlantUnit plantUnit;
	ConsumableItem itemToUse;

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
			if (Keyboard.current.enterKey.wasPressedThisFrame)
			{
				if(unitToUseItemOn.currentHealth >= unitToUseItemOn.maxHealth)
				{
					if (unitToUseItemOn == playerUnit)
						Inventory.instance.textPopup.SetPopup("PLAYER ALREADY HAS FULL HEALTH");
					else
						Inventory.instance.textPopup.SetPopup(unitToUseItemOn.unitName.ToUpper() + " ALREADY HAS FULL HEALTH");
					
					return;
				}


				itemToUse.UseItem(unitToUseItemOn);
				Inventory.instance.RemoveItem(itemToUse);
				unitToUseItemOn.transform.GetChild(1).gameObject.SetActive(false);

				if (unitToUseItemOn == playerUnit)
					StartCoroutine(playerUnit.playerHUD.SetHealthFromItem(playerUnit.currentHealth));
				else
					StartCoroutine(plantUnit.playerPlantHUD.SetHealthFromItem(plantUnit.currentHealth));

				AwaitingTargetToUseItemOn = false;
			}

			if (Keyboard.current.wKey.wasPressedThisFrame)
			{
				unitToUseItemOn = playerUnit;
				playerUnit.transform.GetChild(1).gameObject.SetActive(true);
				plantUnit.transform.GetChild(1).gameObject.SetActive(false);
			}

			if (Keyboard.current.sKey.wasPressedThisFrame)
			{
				unitToUseItemOn = plantUnit;
				plantUnit.transform.GetChild(1).gameObject.SetActive(true);
				playerUnit.transform.GetChild(1).gameObject.SetActive(false);
			}

			if (Keyboard.current.wKey.wasPressedThisFrame)
			{
				unitToUseItemOn.transform.GetChild(1).gameObject.SetActive(false);
				unitToUseItemOn = playerUnit;
				AwaitingTargetToUseItemOn = false;
			}
		}
	}
	public void ChooseTargetToUseItem(ConsumableItem _itemToUse)
	{
		itemToUse = _itemToUse;

		playerUnit.transform.GetChild(1).gameObject.SetActive(true);
		plantUnit.transform.GetChild(1).gameObject.SetActive(false);

		unitToUseItemOn = playerUnit;

		AwaitingTargetToUseItemOn = true;
	}
}
