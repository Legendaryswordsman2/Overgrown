using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStation : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        InputManager.playerInputActions.Player.Interact.performed += Interact_performed;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        InputManager.playerInputActions.Player.Interact.performed -= Interact_performed;
    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OpenSaveMenu();
    }
    void OpenSaveMenu()
    {
        GameManager.instance.playerHealthBar.SetActive(false);
        GameManager.OpenOverlay(GameManager.instance.saveMenu);
    }
}
