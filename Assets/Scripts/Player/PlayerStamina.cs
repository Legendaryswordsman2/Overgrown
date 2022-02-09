using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Adjustements")]
    [SerializeField] int maxStamina = 100;
    [ReadOnlyInspector] public float currentStamina;

    [SerializeField] float staminaRegenDelay = 3, staminaRegenSpeed = 20;

    [Header("References")]
    [SerializeField] ProgressBar staminaBar;

    [SerializeField] TMP_Text staminaBarText;

    bool canRegenerateStamina = true;

    private void Start()
    {
        staminaBar.maximum = maxStamina;
        currentStamina = maxStamina;
    }
    private void Update()
    {
     if (canRegenerateStamina && currentStamina <= maxStamina)
        {
        currentStamina += Time.deltaTime * staminaRegenSpeed;
        staminaBar.current = Convert.ToInt32(currentStamina);
        staminaBarText.text = Convert.ToString(Convert.ToInt32(currentStamina));
        }
    }

    public bool UseStamina(int amount)
    {
        if(currentStamina >= amount)
        {
        currentStamina -= amount;
        staminaBar.current = Convert.ToInt32(currentStamina);
        staminaBarText.text = Convert.ToInt32(currentStamina) + "";
        StopAllCoroutines();
        StartCoroutine(regenerateStaminaWaitTime());
        return true;
        }
        return false;
    }
    IEnumerator regenerateStaminaWaitTime()
    {
        canRegenerateStamina = false;
        yield return new WaitForSeconds(staminaRegenDelay);
        canRegenerateStamina = true;
    }
}
