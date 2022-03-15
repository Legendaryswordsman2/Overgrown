using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthBar : ProgressBar
{
	[SerializeField] TMP_Text healthTextCurrent, healthTextMax;

	protected override void GetCurrentFIll()
	{
		base.GetCurrentFIll();

		if (healthTextCurrent != null && healthTextMax != null)
		{
			healthTextCurrent.text = current.ToString();
			healthTextMax.text = "       / " + maximum.ToString();
		}
	}
}
