using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
	MeshRenderer backgroundMeshRenderer;
	Vector2 textureOffset = Vector2.zero;

	[SerializeField] float speed = 0.05f;

	private void Start()
	{
		backgroundMeshRenderer = GetComponent<MeshRenderer>();
	}
	private void Update()
	{
		textureOffset.x += speed * Time.deltaTime;
		backgroundMeshRenderer.material.mainTextureOffset = textureOffset;
	}
}
