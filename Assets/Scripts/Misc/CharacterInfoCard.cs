using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CharacterInfoCard : MonoBehaviour
{
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text xpText;
    [SerializeField] TMP_Text damageText;
    [SerializeField] TMP_Text defenseText;
    [SerializeField] TMP_Text critChanceText;

    string levelTextString;

    private void Awake()
    {
        levelTextString = levelText.text;
    }

    public void SetInfoCard(PlayerStats stats)
    {
        healthText.text = stats.currentHealth + " / " + stats.maxHealth;

        if (stats.meleeWeapon != null)
            damageText.text = Convert.ToString(stats.damage + stats.meleeWeapon.meleeDamageModifier);
        else
            damageText.text = stats.damage.ToString();

        if (stats.armor != null)
            defenseText.text = Convert.ToString(stats.defense + stats.armor.defenseModifier);
        else
            defenseText.text = stats.defense.ToString();

        critChanceText.text = stats.critChance.ToString();

        levelText.text = levelTextString + stats.playerLevelSystem.level;

        xpText.text = stats.playerLevelSystem.xp + " / " + stats.playerLevelSystem.xpToLevelUp;
    }
    public void SetInfoCard(PlantStats stats)
    {
        healthText.text = stats.currentHealth + " / " + stats.maxHealth;
        damageText.text = stats.damage.ToString();
        defenseText.text = stats.defense.ToString();
        critChanceText.text = stats.critChance.ToString();

        levelText.text = levelTextString + stats.plantLevelSystem.level;

        xpText.text = stats.plantLevelSystem.xp + " / " + stats.plantLevelSystem.xpToLevelUp;
    }
}
