using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAftterTime : MonoBehaviour
{
    [SerializeField]
    float timer = 5;

    private void Start()
    {
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
