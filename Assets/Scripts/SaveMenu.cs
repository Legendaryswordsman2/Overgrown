using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMenu : MonoBehaviour
{
    private void OnEnable()
    {
        InputManager.playerInputActions.Player.Back.performed += CloseSaveMenuFromKeybind;
    }
    private void OnDisable()
    {
        InputManager.playerInputActions.Player.Back.performed -= CloseSaveMenuFromKeybind;
    }
    private void CloseSaveMenuFromKeybind(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        CloseSaveMenu();
    }
    public void CloseSaveMenu()
    {
        InputManager.playerInputActions.Player.Back.performed -= CloseSaveMenuFromKeybind;
        GameManager.CloseOverlay(GameManager.instance.saveMenu);

    }
}
