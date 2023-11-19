using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Enemy1AI : MonoBehaviour
{
    public Transform Playertransform;
    public GameObject Player;
    [SerializeField] private int movespeed = 5;
    [SerializeField] private int damage = 1;
    int attackrange = 2;

    private bool attacked = false;
    bool dead = false;
    [SerializeField] private float fadeSpeed;

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

        if (dead) {
            Debug.Log("ur mum");
            Color col = mesh.material.color;
            Debug.Log(col.a);
            mesh.material.color = new Color(col.r, col.g, col.b, col.a - (fadeSpeed * Time.deltaTime));
            if (col.a <= 0) {
                Destroy(gameObject);
            }
            return;
        }

        transform.LookAt(Playertransform);
        //print(rb.velocity.x);
        if (Vector3.Distance(transform.position, Playertransform.position) >= attackrange)
        {
            // move to player
            if (rb.velocity.magnitude <= movespeed)
            {
                movevector = (Playertransform.position - transform.position).normalized;
                rb.AddForce(movevector * movespeed * Time.fixedDeltaTime * 10f);
            }
        }
        else if (!attacked)
        { 
            attacked = true;
            StartCoroutine(Attack());
            //Here Call any function U want Like attack
        }
    }

    IEnumerator Attack()
    {
        // gameObject.GetComponent<SphereCollider>().enabled = false;
        // mesh.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("dead haha");
        dead = true;
        if (Player.TryGetComponent(out Health playerhit))
        {
            attacksound.PlayOneShot(jumpscareclip);
            attacksound.clip = null;
            playerhit.takedamage(damage);
        }
        Debug.Log("dead haha");
        
        yield return new WaitForSeconds(jumpscareclip.length/2.0f);
    }

}
