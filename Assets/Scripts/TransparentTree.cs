using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TransparentTree : MonoBehaviour
{
    [SerializeField] byte AlphaWhenNear = 100;

    Color32 NearPlayer = new Color32(255, 255, 255, 103);
    Color32 NotNearPlayer = new Color32(255, 255, 255, 255);

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        NearPlayer.a = AlphaWhenNear;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           sr.color = NearPlayer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            sr.color = NotNearPlayer;
        }
    }
}
