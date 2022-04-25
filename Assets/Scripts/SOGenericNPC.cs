using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPCs/Generic NPC", fileName = "New Generic NPC")]
public class SOGenericNPC : ScriptableObject
{
    public string NPCName = "Generic NPC";

    [TextArea]
    public string[] dialogue;
}
