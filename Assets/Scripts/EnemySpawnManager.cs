using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
	[SerializeField] bool showSpawnLocations = true;
	[SerializeField] float spawnLocationMarkerSize = 0.5f;
	private void OnDrawGizmos()
	{
		if (!showSpawnLocations) return;

		foreach (Transform child in transform)
		{
			Gizmos.DrawWireSphere(child.position, spawnLocationMarkerSize);
		}
	}

//	#region Editor

//#if UNITY_EDITOR
//	[CustomEditor(typeof(UIAnimator))]
//	public class UIAnimatorEditor : Editor
//	{
//		public override void OnInspectorGUI()
//		{
//			base.OnInspectorGUI();

//			UIAnimator uiAnimator = (UIAnimator)target;

//			EditorGUILayout.Space();
//			EditorGUILayout.LabelField("Details");

//			EditorGUILayout.BeginHorizontal();

//			EditorGUILayout.ObjectField(uiAnimator.tests, typeof(GameObject), true);

//			uiAnimator.runAllAnimationOnceAtStart = EditorGUILayout.Toggle(uiAnimator.runAllAnimationOnceAtStart);

//			EditorGUILayout.EndHorizontal();
//		}
//	}
//#endif
//	#endregion
}
