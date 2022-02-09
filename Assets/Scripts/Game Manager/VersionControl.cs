using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class VersionControl : MonoBehaviour
{
	[SerializeField] TMP_Text versionText;

	private void Start()
	{
		InitializeVersion();
	}
	void InitializeVersion()
	{
		versionText.text = "ALPHA\nV" + Application.version;
	}
}
