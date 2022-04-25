using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDialogueSystem : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text dialogueText;

    string[] dialogue;
    public void Talk(SOGenericNPC npc)
    {
        nameText.text = npc.NPCName.ToUpper();

        dialogue = npc.dialogue;

        dialogueText.text = dialogue[0].ToUpper();

        gameObject.SetActive(true);
    }
}
