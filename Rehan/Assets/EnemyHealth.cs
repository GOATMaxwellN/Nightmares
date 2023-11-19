using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;

    public void takedamage(float damage)
    {
        health -= damage;

        if (health <= 0.01f)
        {
            // play death
            Destroy(gameObject);
        }
    }
}
