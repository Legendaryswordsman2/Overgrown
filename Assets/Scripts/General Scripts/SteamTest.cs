using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamTest : MonoBehaviour
{
	private void Start()
	{
		if (!SteamManager.Initialized) 
		{ 
			Debug.Log("Steam is not open, please open steam");
			return;
		}

		string name = SteamFriends.GetPersonaName();
		Debug.Log(name);
	}
}
