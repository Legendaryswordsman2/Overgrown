using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    UIAnimator uiAnimator;

    [Header("health")]
    public int maxHealth = 100;
    [field: SerializeField, ReadOnlyInspector] public int currentHealth { get; set; }
    [SerializeField] float invincibilityTimeAfterHurt = 0.5f;


    [SerializeField, Header("Knockback")]
    float knockbackX = 10;
    [SerializeField]
    float knockbackY = 2;

    [Header("References")]
    [SerializeField] TMP_Text healthPercentage;

    [SerializeField] GameObject gameOverScreen;

    [SerializeField] ProgressBar healthBar;


    [HideInInspector]
    public bool playerIsAlive;

    Rigidbody2D rb;

    Animator anim;

    Statistics gameStats;

    [HideInInspector]
    public bool canBeHurt = true;

    private void Start()
    {
        uiAnimator = GameObject.FindGameObjectWithTag("Health Bar Background").GetComponent<UIAnimator>();

        currentHealth = maxHealth;
        healthBar.maximum = maxHealth;
        healthBar.current = currentHealth;
        healthPercentage.text = currentHealth.ToString();
        playerIsAlive = true;
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(4).GetComponent<Animator>();
        gameStats = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<Statistics>();
    }   
    public bool AttackPlayer(int damage, bool leftOrRight)
    {
        if(canBeHurt == true)
        {
            StartCoroutine(uiAnimator.RunAnimation(0));

            currentHealth -= damage;

            healthPercentage.text = currentHealth.ToString();

            StartCoroutine(TempInvincibility());

            healthBar.current = currentHealth;

            anim.SetTrigger("Hurt");

            if (leftOrRight == true)
            {
                rb.velocity = new Vector2(knockbackX, knockbackY);
            }
            else
            {
                rb.velocity = new Vector2(-knockbackX, knockbackY);
            }

            if (currentHealth <= 0)
            {
                die();
            }
            return true;
        }
        return false;
    }

    public void die()
    {
        Time.timeScale = 0f;
        playerIsAlive = false;
        Debug.Log("Player Died");
        gameOverScreen.SetActive(true);
        gameStats.IncreasePlayerDeaths();
    }
    
    IEnumerator TempInvincibility()
    {
        canBeHurt = false;
        yield return new WaitForSeconds(invincibilityTimeAfterHurt);
        canBeHurt = true;
    }

    public void RefreshValues()
    {
        Debug.Log("Refreshed Health bar");
        healthPercentage.text = currentHealth.ToString();
        healthBar.maximum = maxHealth;
        healthBar.current = currentHealth;
    }

}
