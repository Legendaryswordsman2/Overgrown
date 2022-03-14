using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grow : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [Header("Icons")]
    [SerializeField] GameObject plantIcon;
    [SerializeField] GameObject discardIcon;
    [SerializeField] ProgressBar progressBar;

    [SerializeField, ReadOnlyInspector, Header("The total time until the plant finishes growing")]
    int growTime;

    // Scriptable object references
    [Header("Set when the player chooses a plant in game")]
    [ReadOnlyInspector] public PlantData chosenPlant;
    [ReadOnlyInspector] public SOEnemy chosenEnemy;

    // Stages
    bool canPlant = true;
    [HideInInspector]
    bool isGrowing;
    bool startedGrowing;
    bool finishedGrowing;

    // Behind the scenes variables
    Sprite[] currentPlantStages;
    [SerializeField, ReadOnlyInspector]
    List<int> growTimes = new List<int>();
    int nextGrowthStage = 0;
    int amountOfStages;
    GameObject plantUi;
    SpriteRenderer sr;
    bool isInRange = false;
    GameObject monster;
    int monsterWaitTimeAfterGrowth;
    int currentGrowthTimeForProgressBar = 0;
    GameObject enemy;

    // Keys
    KeyCode openPlantMenuKey = KeyCode.E, discardKey = KeyCode.E;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        gameManager = GameManager.instance;

        // Finds a game object with the tag Plant UI then gets the transform component to access the child of the object, but the PlantUI is of type GameObject so we must tranform it back into a game object
        plantUi = gameManager.plantUiMenu;
    }

    private void Update()
    {
        if (startedGrowing == true)
        {
            StartCoroutine(IsGrowing());
        }
        else if (canPlant == true)
        {
            CanPlant();
        }
        else if (finishedGrowing == true)
        {
            FinishedGrowing();
        }

        if(isGrowing == true && isInRange == true && Input.GetKeyDown(discardKey))
        {
            ResetValues();
        }
    }

    void CanPlant()
    {
        if (Input.GetKeyDown(openPlantMenuKey) && isInRange && isGrowing == false)
        {
            GameManager.StopTime();
            plantUi.SetActive(true);
        }
    }
    IEnumerator IsGrowing()
    {
        if (startedGrowing)
        {
            startedGrowing = false;
            StartCoroutine(GrowTimer());
        }
        yield return new WaitForSeconds(growTimes[nextGrowthStage - 1]);
        sr.sprite = currentPlantStages[nextGrowthStage];
        nextGrowthStage++;

        if (nextGrowthStage >= amountOfStages)
        {
            yield return new WaitForSeconds(monsterWaitTimeAfterGrowth);
            isGrowing = false;
            finishedGrowing = true;
        }
        else
        {
            StartCoroutine(IsGrowing());
        }
    }
    void FinishedGrowing()
    {
        ResetValues();
    }
    public void AssignVariables()
    {
        if(canPlant == true)
        {
            canPlant = false;
            isGrowing = true;

                if (isInRange)
                {
                    discardIcon.SetActive(true);
                }

            for (int i = 0; i < chosenPlant.plantGrowthStages.Length - 1; i++)
            {
                growTimes.Add(Random.Range(chosenPlant.plantMinGrowTime, chosenPlant.plantMaxGrowTime));
                growTime += growTimes[i];
            }

            currentPlantStages = chosenPlant.plantGrowthStages;
            //monster = chosenPlant.enemy;
            amountOfStages = currentPlantStages.Length;
            plantIcon.SetActive(false);
            sr.sprite = currentPlantStages[nextGrowthStage];
            nextGrowthStage++;

            monsterWaitTimeAfterGrowth = Random.Range(chosenPlant.minMonsterWaitTimeAfterGrowth, chosenPlant.maxMonsterWaitTimeAfterGrowth);
            growTime += monsterWaitTimeAfterGrowth;

            startedGrowing = true;

            progressBar.gameObject.SetActive(true);
            progressBar.maximum = growTime;
        }
    }
    private void ResetValues()
    {
        canPlant = true;
        isGrowing = false;
        startedGrowing = false;
        finishedGrowing = false;

        if (isInRange)
        {
            plantIcon.SetActive(true);
        }

        sr.sprite = null;
        nextGrowthStage = 0;
        growTime = 0;

        growTimes = new List<int>();
        currentPlantStages = new Sprite[0];

        amountOfStages = 0;
        monsterWaitTimeAfterGrowth = 0;

        progressBar.gameObject.SetActive(false);
        discardIcon.SetActive(false);


        progressBar.current = 0;
        progressBar.maximum = 0;

        currentGrowthTimeForProgressBar = 0;
    }

    IEnumerator GrowTimer()
    {
        yield return new WaitForSeconds(1);
        if(isGrowing == true)
        {
            growTime--;
            currentGrowthTimeForProgressBar++;
            progressBar.current = currentGrowthTimeForProgressBar;
            StartCoroutine(GrowTimer());
        }
        else
        {
            yield break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;

            if (canPlant == true)
            {
                plantIcon.SetActive(true);
            }
            else if (isGrowing == true)
            {
                discardIcon.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;

            if (canPlant == true)
            {
               plantIcon.SetActive(false);
            }
            else if (isGrowing == true)
            {
                discardIcon.SetActive(false);
            }
        }
    }
}
