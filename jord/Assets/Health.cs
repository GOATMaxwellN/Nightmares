using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int fearlevel = 1;
    public int bpm = 1;

    public int health;
    public int maxhealth;

    public UnityEngine.UI.Image[] hearts;
    public UnityEngine.UI.Text bpmtext;

    [SerializeField] private AudioSource heartbeat;

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

        // heartbeat
        heartbeat.pitch = 0.85f + (float)Math.Pow(1.575, fearlevel)/100f;
        bpmtext.text = bpm.ToString();

    }

    public void takedamage(int damage)
    {
        health -= damage;

        if (health <= 0.01f)
        {
            // play death
        }
    }

    public int getfear()
    {
        return fearlevel;
    }

}
