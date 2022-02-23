using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Enemy")]
public class SOEnemy : ScriptableObject
{
	[ReadOnlyInspector]
	public string id;
	public int defaultHealth = 100;
	public string enemyName = "Enemy";

	[Header("Sprites")]
	public Sprite sprite;
	public RuntimeAnimatorController animatorController;

	[Header("XP")]
	public int minXPOnDeath = 2;
	public int maxXPOnDeath = 10;

	[Header("Comabat")]
	public float attackRange = 0.5f;
	public int attackDamage = 20;
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
