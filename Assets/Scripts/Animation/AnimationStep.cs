using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AnimationStep
{
	public AnimationFrame[] frames;
	public bool runAtStart = false;
	public bool loop = false;
}
