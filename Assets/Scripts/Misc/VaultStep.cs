using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VaultStep
{
	public Vector2 vaultStep;
	[Tooltip("The amount of time it takes to compelte this action in seconds")]
	public float duration;
}
