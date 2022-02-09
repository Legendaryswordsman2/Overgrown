using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{

    [Min(0), Header("Adjustements")]
    [SerializeField] float attackRange = 0.5f;
    [Min(0)]
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] LayerMask enemyLayers;

    [Header("References")]
    [SerializeField] Transform attackPoint;

    Animator anim;

    GameManager gameManger;

    Player playerScript;

    [HideInInspector]
    public bool canAttack;

    private void Awake()
    {
        anim = transform.GetChild(4).GetComponent<Animator>();
        playerScript = GetComponent<Player>();
        gameManger = GameManager.instance;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerScript.canWalk && GameManager.timeActive)
        {
            anim.SetTrigger("Attack");
            //Attack();
        }
    }

    public void Attack()
    {
        // Play Attack Animation
        //anim.SetTrigger("Attack");

        // Detect Enemies in range of attack
        Collider2D hitEnemy = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);
        // Damage Enemies
        try
        {
            if (hitEnemy != null)
			{

                GameManager.StopTime();

                SOEnemy[] _enemies = hitEnemy.transform.GetComponentInParent<Enemy>().enemyData.enemiesToFight;
                BattleData.AssignVariables(_enemies, SceneManager.GetActiveScene().buildIndex, transform.position);

                StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", "Turn Based Combat"));
			}

            StartCoroutine(AttackCooldown());
        }
        catch
        {
            Debug.LogError("The enemy " + hitEnemy.name + " does not have an enemy script and can not be damaged");
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
