using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 1f;    
    public float gravity = -9.81f;
    Vector3 velocity;
    GameObject bean;
    // public Rigidbody rb;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        // Debug.Log(velocity);
        // Debug.Log($"egg {Bean.colliding}");
        
        if (Bean.colliding) {
            velocity.y = 0;
        }
    }

    
}
