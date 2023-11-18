using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearOrbs : MonoBehaviour
{

    public Transform player;
    bool canSpawn = true;
    public float spawnDelay;
    public GameObject orb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn) {
            StartCoroutine(Spawn());
        }
    }

    private IEnumerator Spawn() {
        canSpawn = false;
        Instantiate(orb, player.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)), Quaternion.identity);
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
}
