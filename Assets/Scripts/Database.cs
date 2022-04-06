using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Database", fileName = "DataBase")]
public class Database : ScriptableObject
{
	[SerializeField] SOEnemy[] enemies;

	[SerializeField] SOPlant[] plants;

	[SerializeField] SOGrowingPlant[] growingPlants;

	[Header("Items")]
	[SerializeField] EquipablePlantItem[] equippablePlantItems;
	[SerializeField] JunkItem[] junkItems;
	[SerializeField] ConsumableItem[] consumableItems;
	[SerializeField] QuestItem[] questItems;
	[Header("Gear")]
	[SerializeField] Armor[] armorItems;
	[SerializeField] MeleeWeapon[] meleeWeaponItems;

	public JunkItem GetJunkItem(string itemID)
	{
		foreach (JunkItem item in junkItems)
		{
			if (item.ID == itemID)
			{
				return item;
			}
		}
		return null;
	}
	public ConsumableItem GetConsumableItem(string itemID)
	{
		foreach (ConsumableItem item in consumableItems)
		{
			if (item.ID == itemID)
			{
				return item;
			}
		}
		return null;
	}
	public QuestItem GetQuestItem(string itemID)
	{
		foreach (QuestItem item in questItems)
		{
			if (item.ID == itemID)
			{
				return item;
			}
		}
		return null;
	}
	public EquipablePlantItem GetEquippablePlantItem(string itemID)
	{
		foreach (EquipablePlantItem item in equippablePlantItems)
		{
			if (item.ID == itemID)
			{
				return item;
			}
		}
		return null;
	}
	public SOGrowingPlant GetGrowingPlant(string plantID)
	{
		foreach (SOGrowingPlant plant in growingPlants)
		{
			if (plant.id == plantID)
			{
				return plant;
			}
		}
		return null;
	}
	public MeleeWeapon GetMeleeWeaponItem(string itemID)
	{
		foreach (MeleeWeapon item in meleeWeaponItems)
		{
			if (item.ID == itemID)
			{
				return item;
			}
		}
		return null;
	}
	public Armor GetArmorItem(string itemID)
	{
		foreach (Armor item in armorItems)
		{
			if (item.ID == itemID)
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

		growingPlants = FindAssetsByType<SOGrowingPlant>("Assets/Scriptable Objects/Growing Plants");

		plants = FindAssetsByType<SOPlant>("Assets/Scriptable Objects/Plants");

		// Items
		equippablePlantItems = FindAssetsByType<EquipablePlantItem>("Assets/Scriptable Objects/Items/Plant Items");
		junkItems = FindAssetsByType<JunkItem>("Assets/Scriptable Objects/Items/Junk Items");
		consumableItems = FindAssetsByType<ConsumableItem>("Assets/Scriptable Objects/Items/Consumable Items");
		questItems = FindAssetsByType<QuestItem>("Assets/Scriptable Objects/Items/Quest Items");

		// Gear
		armorItems = FindAssetsByType<Armor>("Assets/Scriptable Objects/Items/Gear/Armor");
		meleeWeaponItems = FindAssetsByType<MeleeWeapon>("Assets/Scriptable Objects/Items/Gear/Melee Weapons");
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
