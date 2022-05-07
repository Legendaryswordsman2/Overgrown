using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    #region Assigning Variables
    public static GameManager instance { get; private set; }

    public static PlayerInputActions playerInputActions;

    [field: Header("Player Canvas References")]
    [field: SerializeField] public GameObject pauseMenu { get; private set; }
    [field: SerializeField] public Inventory inventory { get; private set; }
    [field: SerializeField] public GameObject playerLevel { get; private set; }
    [field: SerializeField] public GameObject plantUiMenu { get; private set; }
    [field: SerializeField] public GameObject playerHealthBar { get; private set; }
    [field: SerializeField] public GameObject player { get; private set; }
    [field: SerializeField] public GameObject playerLevelUpScreen { get; private set; }

    [Header("Prefabs")]
    [SerializeField] GameObject enemyTemplate;

    [Header("Keycodes")]
    [SerializeField] KeyCode pauseMenuKey = KeyCode.Escape;
    [SerializeField] KeyCode inventoryKey = KeyCode.I;

    public static Vector3 spawnPosition;

    [field: Header("Allowed Actions")]
    [field: SerializeField] public bool allowedToWalk { get; private set; } = true;

    [field: Space]

    [field: SerializeField] public Database Database { get; private set; }


    // Hidden Variables

    AudioSource audioSource;

    public static GameObject currentlyOpenOverlay { get; private set; }

    public static bool timeActive { get; private set; } = true;

    InventoryInputManager inventoryInputManager;
	#endregion

	#region Unity Methods
	private void Awake()
    {
        instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.OpenPauseMenu.performed += OpenPauseMenu_performed;
        playerInputActions.Player.OpenInventory.performed += OpenInventory_performed;

        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        inventoryInputManager = inventory.GetComponent<InventoryInputManager>();
    }


    private void OpenPauseMenu_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (currentlyOpenOverlay == pauseMenu)
            ClosePauseMenu();
        else
            OpenPauseMenu();
    }
    private void OpenInventory_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (currentlyOpenOverlay == inventory.gameObject)
            CloseInventory();
        else
            OpenInventory();
    }

    public static bool OpenOverlay(GameObject overlayToOpen, bool stopTime = true)
	{
        if (currentlyOpenOverlay != null) return false;

        currentlyOpenOverlay = overlayToOpen;

        currentlyOpenOverlay.SetActive(true);

        if (stopTime) StopTime();

        return true;
	}

    public static bool CloseOverlay(GameObject overlayToClose, bool startTime = true)
	{
        if (overlayToClose == null)
		{
            Debug.LogError("Can't close an overlay that is null");
            return false;
        }

        if(overlayToClose != currentlyOpenOverlay)
		{
            Debug.Log($"The overlay '{overlayToClose}' you're trying to close is already closed");
            return false;
		}


        currentlyOpenOverlay.SetActive(false);

        currentlyOpenOverlay = null;

        if (startTime) StartTime();

        return true;
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

    public void StartBattle(SOEnemy[] enemies, SOEnemy enemyInCombat, bool playerStartsTurn = true)
	{
        EnemySpawnManager enemySpawnManager = EnemySpawnManager.instance;

        bool hasRemovedEnemyForCombat = false;

        for (int i = 0; i < enemySpawnManager.enemiesAlive.Count; i++)
        {
            if (enemySpawnManager.enemiesAlive[i].GetComponent<Enemy>().enemyData != enemyInCombat || hasRemovedEnemyForCombat)
                enemySpawnManager.enemiesAliveSaveData.Add(new EnemySaveData(enemySpawnManager.enemiesAlive[i].GetComponent<Enemy>().enemyData, enemySpawnManager.enemiesAlive[i].transform.position));
            else
                hasRemovedEnemyForCombat = true;
        }

        EnemySaveData[] _enemiesAliveSaveData = enemySpawnManager.enemiesAliveSaveData.ToArray();

        BattleSetupData.AssignVariables(enemies,
            SceneManager.GetActiveScene().buildIndex,
            player.transform.position,
            _enemiesAliveSaveData,
            playerStartsTurn,
            enemySpawnManager.MinEnemyLevel,
            enemySpawnManager.MaxEnemyLevel);

        StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", "Turn Based Combat"));
    }
	#endregion

	#region Random
	//public void StartPlanting(SOGrowingPlant growingPlantSO)
 //   {
 //       plantUiMenu.SetActive(false);

 //       for (int i = 0; i < PotsNearPlayer.publicPotsNearPlayer.Count; i++)
 //       {
 //           PotsNearPlayer.publicPotsNearPlayer[i].GetComponent<Grow>().chosenGrowingPlant = growingPlantSO;
 //           PotsNearPlayer.publicPotsNearPlayer[i].GetComponent<Grow>().chosenEnemy = growingPlantSO.enemy;
 //           PotsNearPlayer.publicPotsNearPlayer[i].GetComponent<Grow>().AssignVariables();
 //       }
 //       StartTime();
 //       GetComponent<Statistics>().IncreasePlantsPlanted();
 //   }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OpenInventory()
    {
        try
        {
		inventoryInputManager.ResetInventoryView(false);
		bool success = OpenOverlay(inventory.gameObject);

		if (success == false) return;

		playerHealthBar.SetActive(false);
		StopTime();
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Inventory not set to an instance, set inventory to active outside of play mode to fix");
            //throw;
        }
	}

    public void CloseInventory()
	{
        bool success = CloseOverlay(inventory.gameObject);

        if (success == false) return;

        Inventory.instance.itemInfoBox.gameObject.SetActive(false);
        playerHealthBar.SetActive(true);
        StartTime();
    }

    void OpenPauseMenu()
    {
		pauseMenu.transform.GetChild(1).gameObject.SetActive(true);
		pauseMenu.transform.GetChild(2).gameObject.SetActive(false);

		bool success = OpenOverlay(pauseMenu, true);
		if (success) playerHealthBar.SetActive(false);
	}
    public void ClosePauseMenu()
	{
        bool success = CloseOverlay(pauseMenu);

        if (success == false) return;

        pauseMenu.transform.GetChild(1).gameObject.SetActive(true);
        pauseMenu.transform.GetChild(2).gameObject.SetActive(false);

        playerHealthBar.SetActive(true);
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
	public GameObject SpawnEnemy(SOEnemy _enemySO, Vector3 position)
	{
        SOEnemy enemySO = Instantiate(_enemySO);
        GameObject enemy = Instantiate(enemyTemplate, position, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.enemyData = enemySO;
        enemyScript.EnemySetup();
        return enemy;
    }
	#endregion
}