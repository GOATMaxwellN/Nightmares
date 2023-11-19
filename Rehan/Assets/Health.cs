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
    public UnityEngine.UI.Image hurtpanel;
    private float hurtfade = .75f;
    private float currenthurtfade = -1.0f;

    private int score;
    [SerializeField] private UnityEngine.UI.Text scoretext;

    [SerializeField] private UnityEngine.UI.Text timetext;
    private float timeelapsed;
    private string timerstring;

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

        // timer
        if (Time.timeScale > 0)
        {
            timeelapsed += Time.deltaTime;
            timerstring = timer(timeelapsed);
            timetext.text = timerstring;
        }

        if (currenthurtfade > 0)
        {
            currenthurtfade -= Time.deltaTime;
            hurtpanel.color = new Color(hurtpanel.color.r, hurtpanel.color.g, hurtpanel.color.b, 0.25f * currenthurtfade/hurtfade);
        }

    }

    public void takedamage(int damage)
    {
        health -= damage;

        if (health <= 0.01f)
        {
            // play death
        }
        else
        {
            currenthurtfade = hurtfade;
        }

    }

    public int getfear()
    {
        return fearlevel;
    }

    public void addscore(int scoreadd)
    {
        score += scoreadd;
        scoretext.text = score.ToString();
    }

    private string timer(float elapsedtime)
    {
        int hours = Mathf.FloorToInt(elapsedtime / 360F);
        int minutes = Mathf.FloorToInt(elapsedtime / 60F);
        int seconds = Mathf.FloorToInt(elapsedtime % 60F);
        return hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

}
