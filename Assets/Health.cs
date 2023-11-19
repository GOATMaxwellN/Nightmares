using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxhealth;

    public UnityEngine.UI.Image[] hearts;

    private void Update()
    {

        if (health > maxhealth)
        {
            health = maxhealth;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health) { hearts[i].color = new Color(0.7f, 0, 0.15f); }
            else { hearts[i].color = new Color(0, 0, 0); }
        }
    }
}
