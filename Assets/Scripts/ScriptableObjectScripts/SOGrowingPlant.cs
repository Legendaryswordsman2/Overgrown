using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Growing Plant", fileName = "New Growing Plant")]
public class SOGrowingPlant : ScriptableObject
{
    [ReadOnlyInspector]
    public string id;

    public Sprite[] plantGrowthStages = { };

    public int plantMinGrowTime = 1, plantMaxGrowTime = 10;

    [Space]
    public int minMonsterWaitTimeAfterGrowth = 2;
    public int maxMonsterWaitTimeAfterGrowth = 10;

    [Space]
    public SOPlant plant;

#if UNITY_EDITOR
    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
#endif
}