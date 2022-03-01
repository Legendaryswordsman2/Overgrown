using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Assigning Variables
    public static GameManager instance { get; private set; }

    [Header("Player Canvas References")]
    [SerializeField] GameObject selectLevelMap;

    [field: SerializeField] public GameObject pauseMenu { get; private set; }
    [field: SerializeField] public GameObject inventoryCanvas { get; private set; }
    [field: SerializeField] public GameObject inventoryAndMapButtons { get; private set; }
    [field: SerializeField] public GameObject playerLevel { get; private set; }

    public GameObject plantUiMenu;

    [field: Header("Inventory References")]
    [field: SerializeField] public GameObject inventory { get; private set; }

    [field: Header("Random References")]
    [field: SerializeField] public Animator transitionAnimator { get; private set; }




    [Header("Prefabs")]
    [SerializeField] GameObject enemyTemplate;

    [Header("Keycodes")]
    [SerializeField] KeyCode pauseMenuKey = KeyCode.Escape;
    [SerializeField] KeyCode inventoryKey = KeyCode.I;

    [field: SerializeField] public KeyCode vaultKey { get; private set; } = KeyCode.Space;

    public static Vector3 spawnPosition;

    // Hidden Variables

    [field: SerializeField, Header("Allowed Actions")]
    public bool allowedToDash { get; private set; } = true;
    [field: SerializeField]
    public bool allowedToAttack { get; private set; } = true;
    [field: SerializeField]
    public bool allowedToWalk { get; private set; } = true;

    public GameObject player { get; private set; }
    AudioSource audioSource;

    public static bool timeActive = true;
	#endregion

	#region Unity Methods
	private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        OpenPauseMenu();

        OpenInventory();
    }
	#endregion

	#region Essentials
    public static void StopTime()
    {
        Time.timeScale = 0f;
        timeActive = false;
    }
    public static void StartTime()
    {
        Time.timeScale = 1f;
        timeActive = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartBattle(SOEnemy[] enemies, bool playerStartsTurn = true)
	{
        BattleSetupData.AssignVariables(enemies, SceneManager.GetActiveScene().buildIndex, player.transform.position, playerStartsTurn);

        StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", "Turn Based Combat"));
    }
	#endregion

	#region Random
	public void StartPlanting(PlantData plant)
    {
        plantUiMenu.SetActive(false);

        for (int i = 0; i < PotsNearPlayer.publicPotsNearPlayer.Count; i++)
        {
            PotsNearPlayer.publicPotsNearPlayer[i].GetComponent<Grow>().chosenPlant = plant;
            PotsNearPlayer.publicPotsNearPlayer[i].GetComponent<Grow>().chosenEnemy = plant.enemy;
            PotsNearPlayer.publicPotsNearPlayer[i].GetComponent<Grow>().AssignVariables();
        }
        StartTime();
        GetComponent<Statistics>().IncreasePlantsPlanted();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OpenInventory()
    {
        if (Input.GetKeyDown(inventoryKey) && pauseMenu.activeSelf == false)
        {
            if (inventoryCanvas.activeSelf == false && selectLevelMap.activeSelf == false)
            {
                inventoryCanvas.SetActive(true);
                playerLevel.SetActive(true);
                inventoryAndMapButtons.SetActive(true);
                StopTime();
            }
            else
            {
                inventoryCanvas.SetActive(false);
                inventoryAndMapButtons.SetActive(false);
                playerLevel.SetActive(false);
                selectLevelMap.SetActive(false);
                StartTime();
            }
        }
    }

    void OpenPauseMenu()
    {
        if (Input.GetKeyDown(pauseMenuKey) && selectLevelMap.activeSelf == false && inventoryCanvas.activeSelf == false)
        {
            StopTime();
            if (pauseMenu.activeSelf == false)
            {
                pauseMenu.transform.GetChild(1).gameObject.SetActive(true);
                pauseMenu.transform.GetChild(2).gameObject.SetActive(false);
                pauseMenu.SetActive(true);
            }
            else
            {
                StartTime();

                pauseMenu.SetActive(false);
                pauseMenu.transform.GetChild(1).gameObject.SetActive(true);
                pauseMenu.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
    public void PlaySound(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }

    public void Destroy(GameObject gameobjectToDestroy)
    {
        Destroy(gameobjectToDestroy);
        
    }
    public static IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        yield return true;
    }
	public GameObject SpawnEnemy(SOEnemy enemySO, Vector3 position)
	{
        GameObject enemy = Instantiate(enemyTemplate, position, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.enemyData = enemySO;
        enemyScript.EnemySetup();
        return enemy;
    }
	#endregion
}