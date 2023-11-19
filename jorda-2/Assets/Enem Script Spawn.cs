using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScriptSpawn : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public GameObject projectile;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public float health;
    public GameObject minion;

    void Awake() {
        // Debug.Log("awake");
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Patroling() {
        if (!walkPointSet) {
            SearchWalkPoint();
        }

        if (walkPointSet) {
            // Debug.Log("i am patrolling");
            // Debug.Log(walkPoint);
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = true;
        } 
    }

    private void SearchWalkPoint() {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            walkPointSet = true;
        } 
    }

    // private void ChasePlayer() {
    //     agent.SetDestination(player.position);
    // }

    private void AttackPlayer() {
        Vector3 runTo = transform.position + ((transform.position - player.position));
        // float distance = Vector3.distance(transform.position, player.position);
        // if (distance < range) {
        agent.SetDestination(runTo);
        // }
        transform.LookAt(player);

        if (!alreadyAttacked) {
            alreadyAttacked = true;
            GameObject minionClone = Instantiate(minion, transform.position, Quaternion.identity);
            minionClone.SetActive(true);
            // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            // rb.AddForce(transform.up * 1f, ForceMode.Impulse);

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack() {
        alreadyAttacked = false;
    }

    public void takedamage(float damage)
    {
        health -= damage;

        if (health <= 0.01f)
        {
            // play death
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        // Debug.Log("wat");
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange) {
                        // Debug.Log("patrol");

            Patroling();
        }

        // if (playerInSightRange && !playerInAttackRange) {
        //     // Debug.Log("chase");
        //     ChasePlayer();
        // }

        if (playerInSightRange) {
            // Debug.Log("attack");
            AttackPlayer();
        }
    }

    // private void OnDrawGizmosSelected() {
    //     Gizmos.
    // }
}
