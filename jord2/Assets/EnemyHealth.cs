using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public GameObject Player;
    [SerializeField] private int score = 1000;

    public void takedamage(float damage)
    {
        health -= damage;

        if (health <= 0.01f)
        {
            // play death
            if (Player.TryGetComponent(out Health playerhit))
            {
                playerhit.addscore(score);
            }
            Destroy(gameObject);
        }
    }
}