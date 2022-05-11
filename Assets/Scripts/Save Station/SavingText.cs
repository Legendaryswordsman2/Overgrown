using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavingText : MonoBehaviour
{
    TMP_Text savingText;

    [SerializeField] string[] texts;
    int textIndex;

    private void Awake()
    {
        savingText = GetComponent<TMP_Text>();

        texts = new string[4];

        texts[0] = savingText.text;
        int deletionIndex = texts[0].IndexOf(".");
        texts[0] = texts[0].Remove(deletionIndex, 1);

        texts[1] = savingText.text;

        int periodIndex = savingText.text.IndexOf(".");

        texts[2] = savingText.text.Insert(periodIndex, ".");

        texts[3] = savingText.text.Insert(periodIndex, "..");
    }
    private void OnEnable()
    {
        StartCoroutine(AdjustText());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator AdjustText()
    {
        if (textIndex >= texts.Length - 1)
            textIndex = 0;
        else
            textIndex++;

        savingText.text = texts[textIndex];

        yield return new WaitForSecondsRealtime(0.5f);

        StartCoroutine(AdjustText());
    }
}
