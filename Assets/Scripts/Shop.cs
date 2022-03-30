using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	bool isInRange;

	[SerializeField] GameObject openShopIcon;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		isInRange = true;
		openShopIcon.SetActive(true);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		isInRange = false;
		openShopIcon.SetActive(false);
	}

	private void Update()
	{
		if(isInRange && Input.GetKeyDown(KeyCode.E))
		{

		}
	}

}
