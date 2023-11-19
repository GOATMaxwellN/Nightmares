using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Movement
    public float speed;
    public float grounddrag;
    private bool moving;

    // Grounded
    public float playerheight;
    public LayerMask whatisground;
    bool grounded;
    public float gravity = 12.0f;
    private float downvelocity = 0;


    public Transform orientation;

    float horizontalinput;
    float verticalinput;

    Vector3 movedirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerheight * 0.5f + 0.2f, whatisground);
        MyInput();

        if (grounded)
        {
            downvelocity = 0;
        }
        else
        {
            downvelocity -= gravity * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    private void MyInput()
    {
        if (Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical")!=0)
        {
            moving = true;
        }
        else 
        { 
            moving = false;
        }

        horizontalinput = Input.GetAxisRaw("Horizontal");
        verticalinput = Input.GetAxisRaw("Vertical");


    }

    private void MovePlayer()
    {
        movedirection = (orientation.forward * verticalinput + orientation.right * horizontalinput);

        if (moving)
        {
            rb.velocity = movedirection.normalized * speed * 10f + orientation.up * downvelocity;
        }
        else
        {
            rb.velocity = Vector3.zero + orientation.up * downvelocity;
        }
        
    }
}
