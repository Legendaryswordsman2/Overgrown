using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[HelpURL("https://docs.google.com/document/d/1VHuq6mezhax5a27v_43JzB336qOwgS2oilcTkuuWHjg/edit")]
public class EnemyAI : MonoBehaviour
{

    [field: Header("Enemy Speeds")]
    [field: SerializeField] public float attackWalkSpeed { get; set; } = 5;
    [field: SerializeField] public float wanderSpeed { get; set; } = 3;

    [field: Header("Enemy Wait Times"), Space]
    [field: SerializeField] public int minWanderWaitTime { get; set; } = 5;
    [field: SerializeField] public int maxWanderWaitTime { get; set; } = 10;

    [SerializeField, Header("Icons"), Space]
    GameObject foundPlayerIconPrefab;
    [SerializeField]
    Transform IconSpawnPosition;

    // Modes
    [SerializeField, ReadOnlyInspector]
    bool wander = true;

    GameObject player;
    AIDestinationSetter enemyBrain;
    AIPath aiPath;
    Transform leftPoint, rightPoint;

    private void Start()
    {
        attackWalkSpeed = Random.Range(attackWalkSpeed, attackWalkSpeed + 1);

        enemyBrain = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();

        player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject;
        //leftPoint = gameObject.transform.GetChild(0);
        //rightPoint = gameObject.transform.GetChild(1);
        StartCoroutine(Wander());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            aiPath.maxSpeed = attackWalkSpeed;
            enemyBrain.target = player.transform;
            Instantiate(foundPlayerIconPrefab, IconSpawnPosition);
            wander = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision is CircleCollider2D)
        {
            enemyBrain.target = this.transform;
            StartCoroutine(Wander());
            wander = true;
        }
    }

    IEnumerator Wander()
    {
        yield return new WaitForSeconds(Random.Range(minWanderWaitTime, maxWanderWaitTime + 1));
        if(wander == true)
        {
            aiPath.maxSpeed = wanderSpeed;
            int random = Random.Range(0, 6);
            if (random == 0)
            {
                enemyBrain.target = leftPoint;
            }
            else if (random == 1)
            {
                enemyBrain.target = rightPoint;
            }
            else if(random >= 2)
            {
                enemyBrain.target = null;
            }

            yield return new WaitForSeconds(5);
            if (wander == true)
            {
                StartCoroutine(Wander());
            }
        }
        else
        {
            yield break;
        }
    }
}
