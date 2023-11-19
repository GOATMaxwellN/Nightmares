using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class teleport : MonoBehaviour
{

    public Transform Playertransform;
    public GameObject Player;
    public Transform Boss;

    [SerializeField] private int movespeed = 50; 
    Rigidbody rb;

    private Vector3 movevector;

    bool canSpawn = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    IEnumerator ExampleCoroutine()
    {
        // Debug.Log("Before WaitForSeconds");
        // Debug.Log("After WaitForSeconds");
        // yield return new WaitForSeconds(5.0f);
        // Vector3 targetPosition = Playertransform.position;

        // yield return new WaitForSeconds(5.0f);
        // rb.position = new Vector3(2.0f, 1.0f, 2.0f);
        // print("pos change");
        Debug.Log("GG");
        Debug.Log(Boss.position);
        movevector = (Playertransform.position - transform.position).normalized;
        rb.AddForce(movevector * movespeed * Time.fixedDeltaTime * 10f);

        yield return new WaitForSeconds(0.4f);
        movevector = (Playertransform.position - transform.position).normalized;
        rb.AddForce(movevector * movespeed * Time.fixedDeltaTime * -10f);
        




        // rb.position = new Vector3(
        // (Mathf.Max(Playertransform.position.x, rb.position.x) + Mathf.Min(Playertransform.position.x, rb.position.x))/2,
        // Playertransform.position.y,
        // (Mathf.Max(Playertransform.position.z, rb.position.z) + Mathf.Min(Playertransform.position.z, rb.position.z))/2
        // );
        canSpawn = false;
        yield return new WaitForSeconds(3f);
        canSpawn = true;

        // print((Mathf.Max(Playertransform.position.x, rb.position.x) - Mathf.Min(Playertransform.position.x, rb.position.x))/2);
        // print((Mathf.Max(Playertransform.position.z, rb.position.z) - Mathf.Min(Playertransform.position.z, rb.position.z))/2);

        // Additional code after the delay
        Debug.Log("Coroutine finished");
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn) StartCoroutine(ExampleCoroutine());
        
    }
}
