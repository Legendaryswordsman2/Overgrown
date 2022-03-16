using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	public static LevelLoader instance { get; private set; }

	Animator anim;

	private void Awake()
	{
		instance = this;
		anim = GetComponent<Animator>();
	}
	public IEnumerator LoadLevelWithTransition(string transitionAnimationStartName, string transitionAnimationEndName, string sceneName, Vector3 newPlayerPosition = new Vector3())
	{
		GameManager.StopTime();

		anim.SetTrigger(transitionAnimationStartName);

		yield return new WaitForSecondsRealtime(anim.GetCurrentAnimatorStateInfo(0).length);

		PlayAnimationOnSceneChange(transitionAnimationEndName, newPlayerPosition);

		SceneManager.LoadScene(sceneName);
		GameManager.StartTime();
	}
	public IEnumerator LoadLevelWithTransition(string transitionAnimationStartName, string transitionAnimationEndName, int sceneIndex, Vector3 newPlayerPosition = new Vector3())
	{
		GameManager.StopTime();

		anim.SetTrigger(transitionAnimationStartName);

		yield return new WaitForSecondsRealtime(anim.GetCurrentAnimatorStateInfo(0).length);

		PlayAnimationOnSceneChange(transitionAnimationEndName, newPlayerPosition);

		SceneManager.LoadScene(sceneIndex);
		GameManager.StartTime();
	}
	public void LoadLevel(string sceneName, string endAnimationName, Vector3 newPlayerPosition = new Vector3())
	{
		PlayAnimationOnSceneChange(endAnimationName, newPlayerPosition);

		SceneManager.LoadScene(sceneName);
	}
	public void LoadLevel(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	void PlayAnimationOnSceneChange(string animationName, Vector3 _newPlayerPosition)
	{
		GameObject animationPlayer = new GameObject("Animation Player");
		PlayTransitionOnSceneChange animationPlayerScript = animationPlayer.AddComponent<PlayTransitionOnSceneChange>();
		animationPlayerScript.transitionTriggerName = animationName;

		//Debug.Log("Assigned new player position: " + _newPlayerPosition);
		animationPlayerScript.newPlayerPosition = _newPlayerPosition;
	}

	public void loadTitleScreenWithCrossFadeAnimation()
	{
		StartCoroutine(loadTitleScreenWithCrossFadeAnimationIenumerator());
	}
	public IEnumerator loadTitleScreenWithCrossFadeAnimationIenumerator()
	{
		PlayAnimationOnSceneChange("CrossFade", new Vector3());

		anim.SetTrigger("CrossFade Start");

		yield return new WaitForSecondsRealtime(anim.GetCurrentAnimatorStateInfo(0).length);

		SceneManager.LoadScene("Title");
	}
}
