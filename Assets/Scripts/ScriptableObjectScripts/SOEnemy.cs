using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Enemy")]
public class SOEnemy : ScriptableObject
{
	[ReadOnlyInspector]
	public string id;
	[Header("Stats")]
	public int defaultHealth = 100;
	public int meleeDamage = 10;
	public int defense = 0;
	public int critChance;
	public string enemyName = "Enemy";

	[Header("Sprites")]
	public Sprite sprite;
	[Tooltip("A list of animator controllers that will be randomly selected each time that enemy is spawned down")]
	public RuntimeAnimatorController[] animatorControllers;
	[HideInInspector] public RuntimeAnimatorController chosenAnimatorController;

	[Header("XP")]
	public int minXPOnDeath = 5;
	public int maxXPOnDeath = 15;
	[HideInInspector] public int XPOnDeath;

	[Header("Money")]
	public int minMoneyOnDeath = 2;
	public int maxMoneyOnDeath = 10;
	[HideInInspector] public int MoneyOnDeath;

	[Header("Comabat")]
	public float attackRange = 0.5f;
	public EnemiesToFight[] enemiesToFight;

	[Header("Walk Speeds")]
	public float attackWalkSpeed = 5;
	public float wanderSpeed = 3;

	[Header("Wait Times")]
	public int minWanderWaitTime = 2;
	public int maxWanderWaitTime = 10;

	[Header("Leveling")]
	public int healthIncreasePerLevelUp = 10;
	public int damageIncreasePerLevelUp = 2;
	public int xpIncreasePerLevelUp = 10;

	private void Awake()
	{
		chosenAnimatorController = animatorControllers[Random.Range(0, animatorControllers.Length)];
		XPOnDeath = Random.Range(minXPOnDeath, maxXPOnDeath + 1);
		MoneyOnDeath = Random.Range(minMoneyOnDeath, maxMoneyOnDeath + 1);
	}

#if UNITY_EDITOR
	private void OnValidate()
	{
		string path = AssetDatabase.GetAssetPath(this);
		id = AssetDatabase.AssetPathToGUID(path);
	}
#endif
	[System.Serializable]
	public class EnemiesToFight
	{
		public SOEnemy[] enemies;
	}
}
