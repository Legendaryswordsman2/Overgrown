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

    [SerializeField] TMP_Text healthTextCurrent, healthTextMax;

    float fillAmount;

    // Update is called once per frame
    void Update()
    {
        GetCurrentFIll();
    }

    void GetCurrentFIll()
    {
        fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;

        if(healthTextCurrent != null && healthTextMax != null)
		{
            healthTextCurrent.text = current.ToString();
            healthTextMax.text = "       / " + maximum.ToString();
		}
    }
}
