using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallMethod : MonoBehaviour
{
	[SerializeField] UnityEvent callMethod;

	public void InvokeMethod()
	{
		callMethod.Invoke();
	}
}
