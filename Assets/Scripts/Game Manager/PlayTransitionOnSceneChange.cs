using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayTransitionOnSceneChange : MonoBehaviour
{
	public string transitionTriggerName { get; set; }
	public Vector3 newPlayerPosition { get; set; }

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);

		SceneManager.sceneLoaded += OnNewSceneLoaded;
	}

	private void OnNewSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		LevelLoader.instance.GetComponent<Animator>().SetTrigger(transitionTriggerName);
		if (newPlayerPosition != null && newPlayerPosition != new Vector3(0, 0, 0))
		{
			GameObject.FindGameObjectWithTag("Player").transform.position = newPlayerPosition;
		}
		SceneManager.sceneLoaded -= OnNewSceneLoaded;
		Destroy(this.gameObject);
	}
}
