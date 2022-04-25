using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericNPC : MonoBehaviour
{
    [SerializeField] SOGenericNPC npc;

    [SerializeField] NPCDialogueSystem dialogueSystem;

    [SerializeField] GameObject highlightIcon;

    bool isInRange;

    private void Update()
    {
        if (isInRange && GameManager.timeActive && Input.GetKeyDown(KeyCode.E))
        {
            highlightIcon.SetActive(false);
            dialogueSystem.Talk(npc);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            if (highlightIcon != null) highlightIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            if (highlightIcon != null) highlightIcon.SetActive(false);
        }
    }
}
