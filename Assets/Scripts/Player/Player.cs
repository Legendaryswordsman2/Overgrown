using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("movement")]
    [SerializeField] float walkSpeed = 10;

    [Header("Level & XP")]
    public int playerLevel = 1;
    public int xp = 0, xpToLevelUp = 100, xpIncreaseOnLevelUp = 100, xpIncreaseIncreaseOnLevelUp = 20;

    [Header("References")]
    [SerializeField] TMP_Text levelText;
    [SerializeField] ProgressBar levelProgressBar;
    [SerializeField] GameObject gainedXPMessageSpawnLocation;
    [SerializeField] GameObject gainedXPMessagePrfab;
    [SerializeField] CharacterController2D controller;

    [Header("Sounds"), Space]
    [SerializeField] float walkSoundWaitTime = 0.3f;

    float horizontalMove = 0f;

    bool canPlayWalkingSound = true;

    [HideInInspector] public bool canWalk { get; set; } = true;

    // Private References
    PlayerHealth playerHealth;
    PlayerStamina stamina;
    Animator anim;
    Rigidbody2D rb;
    AudioClip[] walkSounds;
    AudioSource audioSource;
    GameManager gameManager;
    Transform playerTexture;

    private void Awake()
    {
        //if (GameManager.spawnPosition != null)
        //{
        //    transform.position = GameManager.spawnPosition;
        //    GameManager.spawnPosition = new Vector3();
        //}

    	playerHealth = GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
        anim = transform.GetChild(4).GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        levelProgressBar.maximum = xpToLevelUp;
        levelProgressBar.current = xp;
        stamina = GetComponent<PlayerStamina>();
        gameManager = GameManager.instance;
        playerTexture = transform.GetChild(4);
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;

        if (canWalk && GameManager.timeActive)
        {
            rb.freezeRotation = true;
            AnimatePlayer();
        }
        else
        {
            anim.SetBool("Walk", false);
            rb.velocity = new Vector2(0, 0);
        }
    }
    private void FixedUpdate()
    {
        if (canWalk && GameManager.timeActive)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ChangeWalkSound>())
        {
            walkSounds = collision.GetComponent<ChangeWalkSound>().walkSounds;
        }
	}

    void AnimatePlayer()
    {
        if(horizontalMove != 0)
        {
            anim.SetBool("Walk", true);
            if (walkSounds != null)
            StartCoroutine(PlayWalkingSound());
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    IEnumerator PlayWalkingSound()
    {
        if (canPlayWalkingSound)
        {
            audioSource.clip = walkSounds[Random.Range(0, walkSounds.Length)];
            audioSource.Play();
            canPlayWalkingSound = false;
        }
        else
        {
            yield break;
        }
        yield return new WaitForSeconds(walkSoundWaitTime);
        canPlayWalkingSound = true;
    }
    public void GiveXp(int xpAmount)
    {
        xp += xpAmount;
        levelText.text = "LV: " + playerLevel;
        levelProgressBar.current = xp;
        gainedXPMessagePrfab.transform.GetChild(1).GetComponent<TMP_Text>().text = "Gained " + xpAmount + " XP";
        Instantiate(gainedXPMessagePrfab, gainedXPMessageSpawnLocation.transform);
        if (xp >= xpToLevelUp)
        {
            LevelUp();
        }
    }
    void LevelUp()
    {
        playerLevel++;

        Debug.Log("You Leveled up to level " + playerLevel);
        levelText.text = "LV: " + playerLevel;

        xp -= xpToLevelUp;
        xpToLevelUp += xpIncreaseOnLevelUp;
        xpIncreaseOnLevelUp += xpIncreaseIncreaseOnLevelUp;

        levelProgressBar.maximum = xpToLevelUp;
        levelProgressBar.current = xp;

        TestIfCanLevelUpAgain();
    }
    void TestIfCanLevelUpAgain()
    {
        if (xp >= xpToLevelUp)
        {
            LevelUp();
        }
    }
    public void RefreshValues()
    {
        levelProgressBar.maximum = xpToLevelUp;
        levelProgressBar.current = xp;
        levelText.text = "LV: " + playerLevel;
    }
}
