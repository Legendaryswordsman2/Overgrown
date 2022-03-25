using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotsNearPlayer : MonoBehaviour
{
	[SerializeField, ReadOnlyInspector] GameObject plantablePot;

	GameObject plantUiMenu;

	private void Awake()
	{
		plantUiMenu = GameManager.instance.plantUiMenu;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Pot")
			plantablePot = collision.gameObject;
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Pot")
			plantablePot = null;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && plantablePot != null)
		{
			GameManager.StopTime();
			plantUiMenu.SetActive(true);
		}
	}
}
