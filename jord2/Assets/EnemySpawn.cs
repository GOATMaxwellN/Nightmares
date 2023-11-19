using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    // public NavMeshAgent agent;
    // public Transform player;
    // public LayerMask whatIsGround, whatIsPlayer;

    // public Vector3 walkPoint;
    // bool walkPointSet;
    // public float walkPointRange;
    // public GameObject projectile;

    // public float timeBetweenAttacks;
    // bool alreadyAttacked;

    // public float sightRange, attackRange;
    // public bool playerInSightRange, playerInAttackRange;
    // public float health;
    public GameObject enemy;
    public Transform[] enemySpawnLocationList;
    public float spawnCooldown = 5f;
    bool canSpawn = true;
    public Transform player;
    public float deactivateDistance = 10f;

    void Awake() {


        // Debug.Log("awake");
        // player = GameObject.Find("Player").transform;
        // agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if (canSpawn) {
            StartCoroutine(Spawn());
        }
    }

    public IEnumerator Spawn() {
        canSpawn = false;
        foreach (Transform enemySpawnLocation in enemySpawnLocationList) {
            if (Vector3.Distance(enemySpawnLocation.position, player.position) > deactivateDistance) {
                Debug.Log("can spawn");
                GameObject enemyClone = Instantiate(enemy, enemySpawnLocation.position + new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f)), Quaternion.identity);
                enemyClone.SetActive(true);
            } else {
                Debug.Log("no spawn");
            }
        }

        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
                    // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

    }

    

    // private void OnDrawGizmosSelected() {
    //     Gizmos.
    // }
}
