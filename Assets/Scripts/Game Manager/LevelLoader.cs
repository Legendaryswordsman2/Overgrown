using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	public static LevelLoader instance { get; private set; }

	Animator anim;

	bool changingScenes = false;

	private void Awake()
	{
		instance = this;
		anim = GetComponent<Animator>();
	}
	#region Base Methods
	public IEnumerator LoadLevelWithTransition(string transitionAnimationStartName, string transitionAnimationEndName, string sceneName, Vector3 newPlayerPosition = new Vector3())
	{
		if (changingScenes) yield return null;

		changingScenes = true;

		GameManager.StopTime();

		anim.SetTrigger(transitionAnimationStartName);

		yield return new WaitForSecondsRealtime(anim.GetCurrentAnimatorStateInfo(0).length);

		PlayAnimationOnSceneChange(transitionAnimationEndName, newPlayerPosition);

		SceneManager.LoadScene(sceneName);
		GameManager.StartTime();
	}
	public IEnumerator LoadLevelWithTransition(string transitionAnimationStartName, string transitionAnimationEndName, int sceneIndex, Vector3 newPlayerPosition = new Vector3())
	{
		if (changingScenes) yield return null;

		changingScenes = true;

		GameManager.StopTime();

		anim.SetTrigger(transitionAnimationStartName);

		yield return new WaitForSecondsRealtime(anim.GetCurrentAnimatorStateInfo(0).length);

		PlayAnimationOnSceneChange(transitionAnimationEndName, newPlayerPosition);

		SceneManager.LoadScene(sceneIndex);
		GameManager.StartTime();
	}
	public void LoadLevel(string sceneName, string endAnimationName, Vector3 newPlayerPosition = new Vector3())
	{
		if (changingScenes) return;

		changingScenes = true;

		PlayAnimationOnSceneChange(endAnimationName, newPlayerPosition);

		SceneManager.LoadScene(sceneName);
	}
	public void LoadLevel(string sceneName)
	{
		if (changingScenes) return;

		changingScenes = true;

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

	#endregion
	public void loadTitleScreenWithCrossFadeAnimation()
	{
		StartCoroutine(loadTitleScreenWithCrossFadeAnimationIEnumerator());
	}
	IEnumerator loadTitleScreenWithCrossFadeAnimationIEnumerator()
	{
		if (changingScenes) yield return null;

		changingScenes = true;

		PlayAnimationOnSceneChange("CrossFade", new Vector3());

		anim.SetTrigger("CrossFade Start");

		yield return new WaitForSecondsRealtime(anim.GetCurrentAnimatorStateInfo(0).length);

		SceneManager.LoadScene("Title");
	}

	public void LoadLevelWithCrossFadeTransition(string sceneName)
	{
		StartCoroutine(LoadLevelWithCrossFadeTransitionIEnumerator(sceneName));
	}
	
	IEnumerator LoadLevelWithCrossFadeTransitionIEnumerator(string sceneName)
	{
		if (changingScenes) yield return null;

		changingScenes = true;

		PlayAnimationOnSceneChange("CrossFade", new Vector3());

		anim.SetTrigger("CrossFade Start");

		yield return new WaitForSecondsRealtime(anim.GetCurrentAnimatorStateInfo(0).length);

		SceneManager.LoadScene(sceneName);
	}
}
