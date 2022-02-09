using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotsNearPlayer : MonoBehaviour
{
    public static bool isInRangeToPlant;

    [SerializeField]
    List<GameObject> potsNearPlayer;

    public static List<GameObject> publicPotsNearPlayer { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pot"))
        {
            potsNearPlayer.Add(collision.gameObject);
            publicPotsNearPlayer = potsNearPlayer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pot"))
        {
            potsNearPlayer.Remove(collision.gameObject);
            publicPotsNearPlayer = potsNearPlayer;
        }
    }
}
