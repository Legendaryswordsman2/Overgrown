using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{

    [Min(0), Header("Adjustements")]
    [SerializeField] float attackRange = 0.5f;
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
        CheckIfEnemyIsNear();
    }

    public void CheckIfEnemyIsNear()
    {

        // Detect Enemies in range of attack
        Collider2D hitEnemy = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);
        // Damage Enemies
        try
        {
            if (hitEnemy != null && GameManager.timeActive)
			{

                GameManager.StopTime();

                Enemy tempEnemy = hitEnemy.transform.GetComponentInParent<Enemy>();
                SOEnemy[] _enemies = tempEnemy.enemyData.enemiesToFight[Random.Range(0, tempEnemy.enemyData.enemiesToFight.Length)].enemies;

                gameManger.StartBattle(_enemies, tempEnemy.enemyData);
			}
        }
        catch
        {
            Debug.LogError("The enemy " + hitEnemy.name + " does not have an enemy script and can not be damaged");
        }
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
