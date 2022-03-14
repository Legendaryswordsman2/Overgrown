using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Database", fileName = "DataBase")]
public class Database : ScriptableObject
{
	[SerializeField] SOEnemy[] enemies;

	[SerializeField] PlantData[] plants;

	[SerializeField] EquipablePlantItem[] equippablePlantItems;

	public EquipablePlantItem GetEquippablePlantItem(string itemID)
	{
		EquipablePlantItem item = GetEquippablePlantItemRefernce(itemID);
		return item;
	}

	EquipablePlantItem GetEquippablePlantItemRefernce(string itemID)
	{
		foreach (EquipablePlantItem item in equippablePlantItems)
		{
			Debug.Log("Found Item");
			if(item.ID == itemID)
			{
				return item;
			}
		}
		return null;
	}

#if UNITY_EDITOR
	private void OnValidate()
	{
		Load();
	}

	private void OnEnable()
	{
		EditorApplication.projectChanged -= Load;
		EditorApplication.projectChanged += Load;
	}

	private void OnDisable()
	{
		EditorApplication.projectChanged -= Load;
	}

	private void Load()
	{
		enemies = FindAssetsByType<SOEnemy>("Assets/Scriptable Objects/Enemies");
		plants = FindAssetsByType<PlantData>("Assets/Scriptable Objects/Plants");
		equippablePlantItems = FindAssetsByType<EquipablePlantItem>("Assets/Scriptable Objects/Items/Plant Items");
	}

	// Slightly modified version of this answer: http://answers.unity.com/answers/1216386/view.html
	public static T[] FindAssetsByType<T>(params string[] folders) where T : Object
	{
		string type = typeof(T).Name;

		string[] guids;
		if (folders == null || folders.Length == 0)
		{
			guids = AssetDatabase.FindAssets("t:" + type);
		}
		else
		{
			guids = AssetDatabase.FindAssets("t:" + type, folders);
		}

		T[] assets = new T[guids.Length];

		for (int i = 0; i < guids.Length; i++)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
			assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
		}
		return assets;
	}
#endif
}
