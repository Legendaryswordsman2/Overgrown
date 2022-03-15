using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    [Min(0)]
    public int maximum, current;

    [SerializeField] Image mask;

    float fillAmount;

    // Update is called once per frame
    void Update()
    {
        GetCurrentFIll();
    }

    protected virtual void GetCurrentFIll()
    {
        fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;
    }
}
