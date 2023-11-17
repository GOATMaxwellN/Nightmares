using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bean : MonoBehaviour
{
    public static bool colliding = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "ground")
        {
            colliding = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {

        if (collision.gameObject.tag == "ground")
        {
            colliding = false;
        }
    }
}
