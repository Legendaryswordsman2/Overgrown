using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField] bool interactable = true;
	[Tooltip("The image component this script will utilize")]
	[SerializeField] Image targetGraphic;
	[Tooltip("The sprite of the iamge when the mouse is hovering over it")]
	[SerializeField] Sprite highlightedSprite;
	[Tooltip("The sprite of the image when the button is pressed (Leave blank to use default sprite)")]
	[SerializeField] Sprite pressedSprite;

	[Space]

	[SerializeField] UnityEvent onClick;

	// Private
	Sprite defaultSprite;
	private void Awake()
	{
		defaultSprite = targetGraphic.sprite;
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		if(interactable)
		targetGraphic.sprite = highlightedSprite;
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		if(interactable)
		targetGraphic.sprite = defaultSprite;
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		if (!interactable) return;

		if (pressedSprite != null)
			targetGraphic.sprite = pressedSprite;
		else
			targetGraphic.sprite = defaultSprite;

		onClick.Invoke();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if(interactable)
		targetGraphic.sprite = highlightedSprite;
	}
}
