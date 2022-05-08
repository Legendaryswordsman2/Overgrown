using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStation : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        InputManager.playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        openSaveMenu();
        InputManager.playerInputActions.Player.Back.performed += closeSaveMenu;
    }
    void openSaveMenu()
    {
        GameManager.OpenOverlay(GameManager.instance.saveMenu);
    }
    private void closeSaveMenu(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        InputManager.playerInputActions.Player.Back.performed -= closeSaveMenu;
        GameManager.CloseOverlay(GameManager.instance.saveMenu);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
