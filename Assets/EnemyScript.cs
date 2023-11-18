using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    public float health = 100f;
    public Transform player;
    public float speed = 50f;
    public Rigidbody rb;
    bool touching = false;
    float chargeTime = 4f;
    float flyUpTime = 0.5f;
    bool canCharge = true;
    bool charging = false;
    bool flyUp = false;
    Vector3 playerSaved;
    // Vector3 transformSaved;
    public void Start() {
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

    public void Update() {
        // Debug.Log(player.position);
        if (gameObject.tag == "Charge") {
            if (canCharge) {
                StartCoroutine(Charge());
            }
            else if (flyUp) {

                transform.LookAt(player);
                Vector3 pos = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), speed * Time.deltaTime);
                rb.MovePosition(pos);
            } else if (touching) {
                charging = false;
            } else if (charging) {
                Vector3 pos = Vector3.MoveTowards(transform.position, playerSaved, speed * Time.deltaTime);
                rb.MovePosition(pos);
            }
            
        } else {
            if (!touching) {
                Vector3 pos = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                rb.MovePosition(pos);
                transform.LookAt(player);
            }   
        }
    }

    private IEnumerator Charge() {
        Debug.Log("hoh");
        canCharge = false;
        transform.LookAt(player);
        // speed = 100f;

        playerSaved = player.position;
        flyUp = true;

        yield return new WaitForSeconds(flyUpTime + Random.Range(-0.1f, 0.1f));

        flyUp = false;
        
        charging = true;

        yield return new WaitForSeconds(chargeTime + Random.Range(-1f, 1f));

        canCharge = true;
        charging = false;
        
    }

    
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player") {
            Debug.Log("touching");
            touching = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            touching = false;
        }
    }
}
