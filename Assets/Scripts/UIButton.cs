using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

enum AnimationMode { None, Basic, ScaleWithPunch, ScaleWithShake, ScaleWithSpring, ScaleInOutElastic, ScaleInOutBack, ScaleInOutBounce}
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

	[SerializeField] AnimationMode animationMode = AnimationMode.None;
	[SerializeField] float duration = 0.5f;
	[SerializeField] float scaleSize = 1.1f;
	[Tooltip("NOTE: NOT APPLICABLE TO SOME ANIMATIONS")]
	[SerializeField] float delayBeforeRevertingSize = 0.2f;

	[Space]

	[SerializeField] UnityEvent onClick;

	bool mouseIsOverImage = false;

	[SerializeField] LeanTweenType easeType;

	// Private
	Sprite defaultSprite;
	Vector3 defaultScale;
	private void Awake()
	{
		defaultSprite = targetGraphic.sprite;
		defaultScale = targetGraphic.gameObject.transform.localScale;
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseIsOverImage = true;

		if (!interactable) return;

		targetGraphic.sprite = highlightedSprite;
		StartCoroutine(PlayAnimation());
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		mouseIsOverImage = false;

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
		if (!interactable) return;

		if (mouseIsOverImage)
			targetGraphic.sprite = highlightedSprite;
		else
			targetGraphic.sprite = defaultSprite;
	}

	IEnumerator PlayAnimation()
	{
		LeanTween.cancel(gameObject);
		transform.localScale = defaultScale;

		switch (animationMode)
		{
			case AnimationMode.None:
				break;
			case AnimationMode.Basic:
				LeanTween.scale(targetGraphic.gameObject, transform.localScale * scaleSize, duration / 2)
				.setIgnoreTimeScale(true);

				yield return new WaitForSecondsRealtime(delayBeforeRevertingSize);

				LeanTween.scale(targetGraphic.gameObject, defaultScale, duration / 2)
				.setIgnoreTimeScale(true);
				break;
			case AnimationMode.ScaleWithPunch:
				LeanTween.scale(targetGraphic.gameObject, transform.localScale * scaleSize, duration)
				.setEasePunch()
				.setIgnoreTimeScale(true);
				break;
			case AnimationMode.ScaleWithShake:
				LeanTween.scale(targetGraphic.gameObject, transform.localScale * scaleSize, duration)
				.setEaseShake()
				.setIgnoreTimeScale(true);
				break;
			case AnimationMode.ScaleWithSpring:
				LeanTween.scale(targetGraphic.gameObject, transform.localScale * scaleSize, duration / 2)
				.setEaseSpring()
				.setIgnoreTimeScale(true);

				yield return new WaitForSecondsRealtime(delayBeforeRevertingSize);

				LeanTween.scale(targetGraphic.gameObject, defaultScale, duration / 2)
				.setEaseSpring()
				.setIgnoreTimeScale(true);
				break;
			case AnimationMode.ScaleInOutElastic:
				LeanTween.scale(targetGraphic.gameObject, transform.localScale * scaleSize, duration / 2)
				.setEaseOutElastic()
				.setIgnoreTimeScale(true);

				yield return new WaitForSecondsRealtime(delayBeforeRevertingSize);

				LeanTween.scale(targetGraphic.gameObject, defaultScale, duration / 2)
				.setEaseInElastic()
				.setIgnoreTimeScale(true);
				break;
			case AnimationMode.ScaleInOutBack:
				LeanTween.scale(targetGraphic.gameObject, transform.localScale * scaleSize, duration / 2)
				.setEaseOutBack()
				.setIgnoreTimeScale(true);

				yield return new WaitForSecondsRealtime(delayBeforeRevertingSize);

				LeanTween.scale(targetGraphic.gameObject, defaultScale, duration / 2)
				.setEaseInBack()
				.setIgnoreTimeScale(true);
				break;
			case AnimationMode.ScaleInOutBounce:
				LeanTween.scale(targetGraphic.gameObject, transform.localScale * scaleSize, duration / 2)
				.setEaseOutBounce()
				.setIgnoreTimeScale(true);

				yield return new WaitForSecondsRealtime(delayBeforeRevertingSize);

				LeanTween.scale(targetGraphic.gameObject, defaultScale, duration / 2)
				.setEaseInBounce()
				.setIgnoreTimeScale(true);
				break;
		}
	}

	private void OnDisable()
	{
		mouseIsOverImage = false;
	}

	private void OnValidate()
	{
		if (targetGraphic == null)
			targetGraphic = GetComponent<Image>();
	}
}
