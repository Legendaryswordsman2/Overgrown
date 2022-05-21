using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveMenuCharacterCard : MonoBehaviour
{
    [SerializeField] TMP_Text levelText;
    [SerializeField] ProgressBar xpBar;

    public void SetCard(LevelSystemSaveData level)
    {
        levelText.text = "LV: " + level.level;
        xpBar.max = level.xpToLevelUp;
        xpBar.current = level.xp;

    }
}
