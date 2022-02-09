using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnMouseOver : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] UnityEvent onMouseEnter;
    [SerializeField] UnityEvent onMouseExit;

    private void OnMouseEnter()
    {
        onMouseEnter.Invoke();
    }

    private void OnMouseExit()
    {
        onMouseExit.Invoke();
    }
}
