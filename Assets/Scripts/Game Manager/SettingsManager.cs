using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SettingsManager : MonoBehaviour
{

    [Space]
    public AudioMixer audioMixer;

    Resolution[] resolutions;

    public TMP_Dropdown resolutionDropdown;

    [SerializeField]
    Slider volumeSlider;
    [SerializeField]
    Toggle fullscreenToggle;
    //[SerializeField]
    //TMP_Dropdown graphicsDropdown;

    [SerializeField, Header("Sounds"), Space]
    AudioClip defaultButtonClickSound;

    AudioSource audioSource;

    bool showReferences;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ResolutionSetup();
        AssignValues();
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        if(isFullscreen == true)
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void ResolutionSetup()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void AssignValues()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");


        if (PlayerPrefs.GetInt("Fullscreen") == 0)
        {
            fullscreenToggle.isOn = true;
        }
        else
        {
            fullscreenToggle.isOn = false;
        }
        //graphicsDropdown.value = PlayerPrefs.GetInt("QualityIndex");
    }
    public void PlayDefaultButtonClickSound()
    {
        audioSource.clip = defaultButtonClickSound;
        audioSource.Play();
    }

    public void SetSaveLocation(int saveSlotIndex)
    {
        if (saveSlotIndex == 1)
        {
            SaveSystem.currentSaveLocation = SaveSystem.saveOneLocation + "/";
        }
        else if (saveSlotIndex == 2)
        {
            SaveSystem.currentSaveLocation = SaveSystem.saveTwoLocation + "/";
        }
        else if (saveSlotIndex == 3)
        {
            SaveSystem.currentSaveLocation = SaveSystem.saveThreeLocation + "/";
        }
        //SaveSystem.CreateSaveSlotSubFolders();
    }

    #region Editor
#if UNITY_EDITOR

    [CustomEditor(typeof(SettingsManager))]
    public class SettingsManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SettingsManager settingsManager = (SettingsManager)target;

            //EditorGUILayout.Space();

            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField("Audio Mixer", GUILayout.MaxWidth(344));
            //settingsManager.audioMixer = (AudioMixer)EditorGUILayout.ObjectField(settingsManager.audioMixer, typeof(AudioMixer), true);
            //EditorGUILayout.EndHorizontal();

            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField("Resolution Dropdown", GUILayout.MaxWidth(344));
            //settingsManager.resolutionDropdown = (TMP_Dropdown)EditorGUILayout.ObjectField(settingsManager.resolutionDropdown, typeof(TMP_Dropdown), true);
            //EditorGUILayout.EndHorizontal();

            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField("Volume Slider", GUILayout.MaxWidth(344));
            //settingsManager.volumeSlider = (Slider)EditorGUILayout.ObjectField(settingsManager.volumeSlider, typeof(Slider), true);
            //EditorGUILayout.EndHorizontal();

            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField("Fullscreen Toggle", GUILayout.MaxWidth(344));
            //settingsManager.fullscreenToggle = (Toggle)EditorGUILayout.ObjectField(settingsManager.fullscreenToggle, typeof(Toggle), true);
            //EditorGUILayout.EndHorizontal();
        }
    }
#endif
#endregion
}
