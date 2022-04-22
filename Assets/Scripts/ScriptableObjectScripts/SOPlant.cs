using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
	[ReadOnlyInspector] public int level = 1;
	public int defaultHealth = 100;
	[ReadOnlyInspector] public int currentHealth = 0;
	public int damage = 10;
	public int defense = 0;
	public int critChance = 0;

	public RuntimeAnimatorController animatorController;

    [Space]

    [Header("XP")]
    [ReadOnlyInspector] public int xp = 0;
    public int xpToLevelUp = 100, xpIncreaseOnLevelUp = 100, xpIncreaseIncreaseOnLevelUp = 20;

	private void Awake()
	{
		currentHealth = defaultHealth;
	}

	public bool GiveXP(int amount)
    {
        xp += amount;

        //if (levelProgressBar != null)
        //{
        //    levelText.text = "LV: " + playerLevel;
        //    levelProgressBar.current = xp;

        //}

        if (xp >= xpToLevelUp)
        {
            LevelUp();
            return true;
        }
        else
            return false;
    }

    private void LevelUp()
    {
		Debug.Log(unitName + " leveled up!");

        level++;

        //if (levelText != null) levelText.text = "LV: " + playerLevel;

        xp -= xpToLevelUp;

        if (xp < 0) xp = 0;

        xpToLevelUp += xpIncreaseOnLevelUp;
        xpIncreaseOnLevelUp += xpIncreaseIncreaseOnLevelUp;

        int[] statIncreases = new int[4];

        for (int i = 0; i < statIncreases.Length; i++)
        {
            statIncreases[i] = UnityEngine.Random.Range(1, 5);
        }

        PlayerStats.instance.playerLevelUpScreen.SetPlantLevelUpScreen(this, statIncreases);

        defaultHealth += statIncreases[0];
        currentHealth += statIncreases[0];
        damage += statIncreases[1];
        defense += statIncreases[2];
        critChance += statIncreases[3];

        //if (levelProgressBar != null)
        //{
        //    levelProgressBar.max = xpToLevelUp;
        //    levelProgressBar.current = xp;
        //}

        //int[] statIncreases = new int[4];

        //for (int i = 0; i < statIncreases.Length; i++)
        //{
        //    statIncreases[i] = UnityEngine.Random.Range(1, 5);
        //}

        //PlayerStats.instance.IncreaseStatsFromLevelUp(statIncreases);

        //TestIfCanLevelUpAgain();
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
	{
		string path = AssetDatabase.GetAssetPath(this);
		id = AssetDatabase.AssetPathToGUID(path);
	}
#endif
}
