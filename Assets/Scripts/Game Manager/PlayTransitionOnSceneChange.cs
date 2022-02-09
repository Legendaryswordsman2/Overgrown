using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTransitionOnSceneChange : MonoBehaviour
{
	public string transitionTriggerName { get; set; }
	public Vector3 newPlayerPosition { get; set; }

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	private void OnLevelWasLoaded(int level)
	{
		LevelLoader.instance.GetComponent<Animator>().SetTrigger(transitionTriggerName);
		if (newPlayerPosition != null && newPlayerPosition != new Vector3(0, 0, 0))
		{
			GameObject.FindGameObjectWithTag("Player").transform.position = newPlayerPosition;
			Debug.Log("Set player position: " + newPlayerPosition);
		}
		Destroy(this.gameObject);
	}
}
