using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
	MeshRenderer backgroundMeshRenderer;
	Vector2 textureOffset = Vector2.zero;

	[SerializeField] float speed = 1;
	Player player;

	private void Start()
	{
		backgroundMeshRenderer = GetComponent<MeshRenderer>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	private void Update()
	{
		if (!player.isWalking)
		{
		textureOffset.x += speed * Time.deltaTime;
		backgroundMeshRenderer.material.mainTextureOffset = textureOffset;
		}
	}
}
