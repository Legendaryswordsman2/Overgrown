using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("movement")]
    [SerializeField] float walkSpeed = 40;

    [Header("References")]
    [SerializeField] CharacterController2D controller;

    [Header("Sounds"), Space]
    [SerializeField] float walkSoundWaitTime = 0.4f;

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

    public bool isWalking { get; private set; }

    private void Awake()
    {

        // Initialize values and references
    	playerHealth = GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        stamina = GetComponent<PlayerStamina>();
        anim = transform.GetChild(4).GetComponent<Animator>();
        playerTexture = transform.GetChild(4);
        gameManager = GameManager.instance;
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
            isWalking = true;
            if (walkSounds != null)
            StartCoroutine(PlayWalkingSound());
        }
        else
        {
            anim.SetBool("Walk", false);
            isWalking = false;
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
}
