using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

[HelpURL("https://docs.google.com/document/d/1VHuq6mezhax5a27v_43JzB336qOwgS2oilcTkuuWHjg/edit")]
public class Enemy : MonoBehaviour
{

    [Header("Scriptable Object References"), Space]
    public SOEnemy enemyData;
    [Header("References")]
    [SerializeField] Animator animator;
    [SerializeField] Transform attackPoint;

    [Min(0), Header("Adjustements")]
    [SerializeField] float attackRange = 0.5f;
    [Min(0)]    [SerializeField] LayerMask playerLayers;

    // Private References
    Rigidbody2D rb;
    AIPath aiPath;
    Player player;
    Statistics gameStats;

    // Start is called before the first frame update
    void Start()
    {

        // Find References
        aiPath = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameStats = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<Statistics>();
    }

    private void Update()
    {
        Attack();
        Animate();
    }
    void Animate()
    {
        if (aiPath.desiredVelocity.x > 0)
        {
        transform.localScale = new Vector3(1, this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (aiPath.desiredVelocity.x < 0)
        {
        transform.localScale = new Vector3(-1, this.transform.localScale.y, this.transform.localScale.z);
        }


        if(aiPath.desiredVelocity.x != 0)
        {
               animator.SetBool("Walk", true);
        }
        else if( aiPath.desiredVelocity.x == 0)
        {
            animator.SetBool("Walk", false);
        }
    }

    public void EnemySetup()
    {
		if (enemyData != null)
        {
        GameObject enemyTextureChild = transform.GetChild(0).gameObject;
        Enemy enemyScript = GetComponent<Enemy>();
        EnemyAI AiBrain = GetComponent<EnemyAI>();

        name = enemyData.enemyName + " (Enemy)";

        // Sprites
        try
        {
            //enemyTextureChild.GetComponent<SpriteRenderer>().sprite = enemyData.sprite;
            enemyTextureChild.GetComponent<Animator>().runtimeAnimatorController = enemyData.animatorController;
        }
        catch
        {
            if (enemyData.sprite == null)
            {
                Debug.Log(enemyData.enemyName + "'s sprite is null");
            }
            if (enemyData.animatorController == null)
            {
                Debug.Log(enemyData.enemyName + "'s animator Controller is null");
            }
        }


        // Walk Speeds
        AiBrain.attackWalkSpeed = enemyData.attackWalkSpeed;
        AiBrain.wanderSpeed = enemyData.wanderSpeed;

        // Wait Times
        AiBrain.minWanderWaitTime = enemyData.minWanderWaitTime;
        AiBrain.maxWanderWaitTime = enemyData.maxWanderWaitTime;
		}
    }

    public void Attack()
    {
        // Play Attack Animation
        //anim.SetTrigger("Attack");

        // Detect Enemies in range of attack
        Collider2D hitEnemy = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayers);
        // Damage Enemies
        try
        {
            if (hitEnemy != null)
            {
                SOEnemy[] _enemies = enemyData.enemiesToFight[Random.Range(0, enemyData.enemiesToFight.Length)].enemies;
                BattleSetupData.AssignVariables(_enemies, SceneManager.GetActiveScene().buildIndex, player.transform.position, false);

                StartCoroutine(LevelLoader.instance.LoadLevelWithTransition("Battle Start", "Battle", "Turn Based Combat"));
            }
        }
        catch
        {
            Debug.LogError("The enemy " + hitEnemy.name + " does not have an enemy script and can not be damaged");
        }
    }

    private void OnValidate()
	{
        EnemySetup();
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
