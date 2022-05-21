using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMenu : MonoBehaviour
{
    [SerializeField] float savingPopupDuration = 3f;
    [SerializeField] float savedPopupDuration = 0.5f;

    [Space]
    
    [SerializeField] GameObject savingPopup;
    [SerializeField] GameObject savedPopup;

    [Space]

    [SerializeField] SaveMenuCharacterCard playerInfoCard;
    [SerializeField] SaveMenuCharacterCard plantInfoCard;
    void SetCharacterCards()
    {
        //PlayerStatsSaveData playerStats = SaveSystem.LoadFileInMainSave<PlayerStatsSaveData>("/Player/Characters/PlayerStats");
        LevelSystemSaveData playerLevel = SaveSystem.LoadFileInMainSave<LevelSystemSaveData>("/Player/Characters/PlayerLevel");

        //PlantStatsSaveData plantStats = SaveSystem.LoadFileInMainSave<PlantStatsSaveData>("/Player/Characters/PlantStats");
        LevelSystemSaveData plantLevel = SaveSystem.LoadFileInMainSave<LevelSystemSaveData>("/Player/Characters/PlantLevel");

        playerInfoCard.SetCard(playerLevel);
        plantInfoCard.SetCard(plantLevel);
    }

    public void SaveAndContinue()
    {
        savingPopup.SetActive(true);
        SaveManager.instance.MakeTempMainSave();

        StartCoroutine(SwitchToSavedPopup(false));
    }
    public void SaveAndQuit()
    {
        savingPopup.SetActive(true);
        SaveManager.instance.MakeTempMainSave();

        StartCoroutine(SwitchToSavedPopup(true));
    }

    IEnumerator SwitchToSavedPopup(bool quitGame)
    {
        yield return new WaitForSecondsRealtime(savingPopupDuration);
        SetCharacterCards();
        savingPopup.SetActive(false);
        savedPopup.SetActive(true);

        yield return new WaitForSecondsRealtime(savedPopupDuration);

        if (quitGame)
        {
            StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("CrossFade Start", "CrossFade", "Title"));
        }
        else
        {
            CloseSaveMenu();
        }
    }
    private void OnEnable()
    {
        InputManager.playerInputActions.Player.Back.performed += CloseSaveMenuFromKeybind;

        SetCharacterCards();

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
