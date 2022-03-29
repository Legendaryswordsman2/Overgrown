using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthBar : ProgressBar
{
	[SerializeField] TMP_Text healthText;

	protected override void GetCurrentFIll()
	{
		base.GetCurrentFIll();

		if (healthText == null) return;

		healthText.text = current + " / " + max;
	}
}
