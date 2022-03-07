using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneOnClick : MonoBehaviour
{
    [Header("Note: this script requires there to be a Game Manager in the scene")]
    [SerializeField] GameObject changeSceneIcon;
    [SerializeField] KeyCode changeSceneKey = KeyCode.E;
    [SerializeField] string sceneName;
    [SerializeField] string transitionEndAnimation;
    [SerializeField] Vector3 newPlayerPosition;
    [SerializeField] bool hasStartTransition = true;

    bool isInRange;
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.instance;
    }
    private void Update()
    {
        if (isInRange && GameManager.timeActive && Input.GetKeyDown(changeSceneKey))
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
            if(changeSceneIcon != null) changeSceneIcon.SetActive(true);
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
        if (changeSceneIcon != null) changeSceneIcon.SetActive(false);
    }
}
