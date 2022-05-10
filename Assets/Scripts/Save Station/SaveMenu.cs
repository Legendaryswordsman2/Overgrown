using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMenu : MonoBehaviour
{
    [SerializeField] float savedPopupDuration = 3;
    [SerializeField] float savingPopupDuration = 0.5f;

    [Space]
    
    [SerializeField] GameObject savingPopup;
    [SerializeField] GameObject savedPopup;

    public void SaveAndContinue()
    {
        savingPopup.SetActive(true);
        SaveManager.instance.MakeTempMainSave();

        StartCoroutine(SwitchToSavedPopup(false));
    }
    public void SaveAndQuit()
    {

    }

    IEnumerator SwitchToSavedPopup(bool quitGame)
    {
        yield return new WaitForSecondsRealtime(savedPopupDuration);
        savingPopup.SetActive(false);
        savedPopup.SetActive(true);

        yield return new WaitForSecondsRealtime(savingPopupDuration);

        if (quitGame)
        {
            
        }
        else
        {
            CloseSaveMenu();
        }
    }
    private void OnEnable()
    {
        InputManager.playerInputActions.Player.Back.performed += CloseSaveMenuFromKeybind;

    }
    private void OnDisable()
    {
        InputManager.playerInputActions.Player.Back.performed -= CloseSaveMenuFromKeybind;
        savedPopup.SetActive(false);
        savingPopup.SetActive(false);
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
