using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1AI : MonoBehaviour
{
    public Transform Player;
    [SerializeField] private int movespeed = 5;
    int attackrange = 2;

    Rigidbody rb;
    private Vector3 movevector;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.LookAt(Player);
        //print(rb.velocity.x);
        if (Vector3.Distance(transform.position, Player.position) >= attackrange)
        {
            // move to player
            if (rb.velocity.magnitude <= movespeed)
            {
                movevector = (Player.position - transform.position).normalized;
                rb.AddForce(movevector * movespeed * Time.fixedDeltaTime * 10f);
            }
        }
        else
        {
            Destroy(gameObject);
            //Here Call any function U want Like attack
        }
    }

}
