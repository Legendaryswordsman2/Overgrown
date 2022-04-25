using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Various/Growing Plant", fileName = "New Growing Plant")]
public class SOGrowingPlant : ScriptableObject
{
    [ReadOnlyInspector]
    public string id;

    public Sprite[] plantGrowthStages = { };

    public int plantGrowTime = 20;

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