using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class UIAnimator : MonoBehaviour
{
	[SerializeField] AnimationStep[] animations;

    bool runAllAnimationOnceAtStart = false;

	Image imageScript;
	Sprite originalSprite;
	GameObject tests;


	private void Start()
	{
		imageScript = GetComponent<Image>();
		originalSprite = imageScript.sprite;
		if (runAllAnimationOnceAtStart)
		{
			StartCoroutine(RunAllAnimations());
		}
		else
		{
			for (int i = 0; i < animations.Length; i++)
			{
				if (animations[i].runAtStart)
					StartCoroutine(RunAnimation(i));
			}
		}
	}

	IEnumerator RunAllAnimations()
	{
		for (int i = 0; i < animations.Length; i++)
		{
			yield return new WaitForSeconds(2);

			for (int i2 = 0; i2 < animations[i].frames.Length; i2++)
		    {
			imageScript.sprite = animations[i].frames[i2].sprite;
			yield return new WaitForSeconds(animations[i].frames[i2].duration);
		    }
		} 
	}

	public IEnumerator RunAnimation(int index)
	{
		for (int i = 0; i < animations[index].frames.Length; i++)
		{
			imageScript.sprite = animations[index].frames[i].sprite;
			yield return new WaitForSeconds(animations[index].frames[i].duration);
		}
			if (animations[index].loop)
		    {
				StartCoroutine(RunAnimation(index));
		    }
		    else
		    {
			imageScript.sprite = originalSprite;
		    }
	}
	#region Editor

#if UNITY_EDITOR
	[CustomEditor(typeof(UIAnimator))]
	public class UIAnimatorEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			UIAnimator uiAnimator = (UIAnimator)target;

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Details");

			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.ObjectField(uiAnimator.tests, typeof(GameObject), true);

			uiAnimator.runAllAnimationOnceAtStart = EditorGUILayout.Toggle(uiAnimator.runAllAnimationOnceAtStart);

			EditorGUILayout.EndHorizontal();
		}
	}
#endif
#endregion
}