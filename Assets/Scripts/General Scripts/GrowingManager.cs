using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingManager : MonoBehaviour
{
	[SerializeField, ReadOnlyInspector] GameObject plantablePot;

	GameObject plantUiMenu;

	private void Awake()
	{
		plantUiMenu = GameManager.instance.plantUiMenu;

        GameManager.playerInputActions.Player.Interact.performed += Interact_performed;
        GameManager.playerInputActions.Player.Back.performed += Back_performed;
	}


    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
		if (plantablePot != null)
		{
			GameManager.OpenOverlay(plantUiMenu);
		}
	}
    private void Back_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
		if (GameManager.currentlyOpenOverlay == plantUiMenu)
			GameManager.CloseOverlay(plantUiMenu);
	}

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Pot"))
		{
			plantablePot = collision.gameObject;
			plantablePot.GetComponentInParent<Grow>().plantIcon.SetActive(true);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Pot"))
		{
			plantablePot.GetComponentInParent<Grow>().plantIcon.SetActive(false);
			plantablePot = null;
		}
	}
	public void StartPlanting(SOGrowingPlant growingPlantSO)
	{
		GameManager.CloseOverlay(plantUiMenu);
		plantablePot.GetComponentInParent<Grow>().StartGrowing(growingPlantSO);
	}
}
