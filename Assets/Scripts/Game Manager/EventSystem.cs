using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
	public static EventSystem instance { get; private set; }

	private void Awake()
	{
		instance = this;
	}

	//public event EventHandler OnPlantEquipped;

}
