using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollision : MonoBehaviour
{
	[Tooltip("The game object that collides with this game object must have the specified tag")]
	[SerializeField] string collisionTag;
	
	[SerializeField] UnityEvent onCollisionEnter;
	[SerializeField] UnityEvent onCollisionExit;

	[SerializeField] UnityEvent onTriggerEnter;
	[SerializeField] UnityEvent onTriggerExit;

	[SerializeField] KeyCode buttonPressKey = KeyCode.None;
	[SerializeField] UnityEvent onTriggerEnterAndButtonPress;

	bool isInRanged = false;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collisionTag == "" || !collision.gameObject.CompareTag(tag)) return;

		Debug.Log("Collision Entered");
		onCollisionEnter.Invoke();
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collisionTag == "" || !collision.gameObject.CompareTag(tag)) return;

		Debug.Log("Collision Exited");
		onCollisionExit.Invoke();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collisionTag == "" || !collision.gameObject.CompareTag(tag)) return;

		Debug.Log("Trigger Entered");
		isInRanged = true;
		onTriggerEnter.Invoke();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collisionTag == "" || !collision.gameObject.CompareTag(tag)) return;

		Debug.Log("Trigger Exited");
		isInRanged = false;
		onTriggerExit.Invoke();
	}

	private void Update()
	{
		if (!isInRanged || buttonPressKey == KeyCode.None) return;

		if (Input.GetKeyDown(buttonPressKey))
		{
			Debug.Log("Trigger Entered And Button Press");
			onTriggerEnterAndButtonPress.Invoke();
		}
	}
}
