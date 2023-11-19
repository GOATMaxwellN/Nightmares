using System.Collections;
using System.Collections.Generic;
// using UnityEditor.PackageManager;
using UnityEngine;

public class Enemy1AI : MonoBehaviour
{
    public Transform Playertransform;
    public GameObject Player;
    [SerializeField] private int movespeed = 5;
    [SerializeField] private int damage = 1;
    int attackrange = 2;

    private bool attacked = false;

    [SerializeField] private AudioSource attacksound;
    [SerializeField] private AudioClip jumpscareclip;

    [SerializeField] private MeshRenderer mesh;
    Rigidbody rb;
    private Vector3 movevector;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (attacked) { return; }

        transform.LookAt(Playertransform);
        //print(rb.velocity.x);
        if (Vector3.Distance(transform.position, Playertransform.position) >= attackrange)
        {
            // move to player
            if (rb.velocity.magnitude <= movespeed && Time.timeScale != 0)
            {
                movevector = (Playertransform.position - transform.position).normalized;
                rb.AddForce(movevector * movespeed * Time.fixedDeltaTime * 10f);
            }
        }
        else
        {
            attacked = true;
            StartCoroutine(Attack());
            //Here Call any function U want Like attack
        }
    }

    IEnumerator Attack()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        mesh.GetComponent<MeshRenderer>().enabled = false;

        if (Player.TryGetComponent(out Health playerhit))
        {
            attacksound.PlayOneShot(jumpscareclip);
            attacksound.clip = null;
            playerhit.takedamage(damage);
        }
        yield return new WaitForSeconds(jumpscareclip.length/2.0f);
        Destroy(gameObject);
    }

}