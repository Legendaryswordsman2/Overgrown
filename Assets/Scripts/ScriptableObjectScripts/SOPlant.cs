using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Plant", fileName = "New Plant")]
public class SOPlant : ScriptableObject
{
	[SerializeField, ReadOnlyInspector] string id;
	public string ID { get { return id; } }
	[Header("Unit Settings")]
	public string unitName = "Some Random Plant";
	public int defaultHealth = 100;
	public int attackDamage = 10;
	public int defense = 0;
	public int critChance = 0;

	public RuntimeAnimatorController animatorController;

#if UNITY_EDITOR
	protected virtual void OnValidate()
	{
		string path = AssetDatabase.GetAssetPath(this);
		id = AssetDatabase.AssetPathToGUID(path);
	}
#endif
}
