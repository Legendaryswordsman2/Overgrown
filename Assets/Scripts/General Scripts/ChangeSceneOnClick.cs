using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneOnClick : MonoBehaviour
{
    [Header("Note: this script requires there to be a Game Manager in the scene")]
    [SerializeField] GameObject changeSceneIcon;
    [SerializeField] string sceneName;
    [SerializeField] string transitionEndAnimation;
    [SerializeField] Vector3 newPlayerPosition;
    [SerializeField] bool hasStartTransition = true;
    

    [SerializeField] bool isInRange;
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        GameManager.playerInputActions.Player.Interact.performed -= Interact_performed;
        if (isInRange && GameManager.timeActive)
        {
            if (hasStartTransition)
            {
                StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("CrossFade Start", transitionEndAnimation, sceneName, newPlayerPosition));
            }
            else
            {
                LevelLoader.instance.LoadLevel(sceneName, transitionEndAnimation, newPlayerPosition);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
			if (changeSceneIcon != null) changeSceneIcon.SetActive(true);
            GameManager.playerInputActions.Player.Interact.performed += Interact_performed;
            isInRange = true;
		}
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.playerInputActions.Player.Interact.performed -= Interact_performed;
            isInRange = false;
			if (changeSceneIcon != null) changeSceneIcon.SetActive(false);
		}
    }
}
