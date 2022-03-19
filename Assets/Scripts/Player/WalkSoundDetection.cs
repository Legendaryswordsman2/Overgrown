using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSoundDetection : MonoBehaviour
{
    Player player;
	private void Awake()
	{
        player = GetComponentInParent<Player>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ChangeWalkSound>())
        {
            player.walkSounds = collision.GetComponent<ChangeWalkSound>().walkSounds;
        }
    }
}
