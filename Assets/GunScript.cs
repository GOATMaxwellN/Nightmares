using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject gun;

    private Camera cam;
    [SerializeField] private AudioSource gunsoundsource;
    [SerializeField] private AudioClip revolvershot;

    [SerializeField] private LayerMask enemylayer;
    [SerializeField] private float damage;
    [SerializeField] private float firerate;
    private float timebeforeshooting;
    private float timeoflastshot = 0f;

    void Start()
    {
        timebeforeshooting = 1 / firerate;
        cam = Camera.main;
    }

    void Update()
    {
        timeoflastshot += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if (timebeforeshooting <= 0f)
            {
                shoot();
                timebeforeshooting = 1 / firerate;
            }
            else
            {
                timebeforeshooting -= Time.deltaTime;
            }
        }
        else if (Input.GetMouseButtonDown(0) && timeoflastshot >= timebeforeshooting)
        {
            timeoflastshot = 0f;
            shoot();
        }
        else
        {
            timebeforeshooting -= Time.deltaTime;
        }
    }

    private void shoot()
    {
        StartCoroutine(StartRecoil());
        gunsoundsource.PlayOneShot(revolvershot);

        Ray gunray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(gunray, out RaycastHit hitInfo, 150f, enemylayer))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out EnemyScript enemyhit))
            {
                enemyhit.takedamage(damage);
                print(enemyhit.health);
            }
        }
    }

    IEnumerator StartRecoil()
    {
        gun.GetComponent<Animator>().Play("Recoil");
        yield return new WaitForSeconds((1 / firerate)*0.9f);
        gun.GetComponent<Animator>().Play("New State");
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private AudioSource gunsoundsource;
    [SerializeField] private AudioClip revolvershot;

    [SerializeField] private LayerMask enemylayer;
    [SerializeField] private float damage;
    [SerializeField] private float firerate;
    private float timebeforeshooting;
    private float timeoflastshot = 0f;

    void Start()
    {
        timebeforeshooting = 1 / firerate;

        cam = Camera.main;
    }

    void Update()
    {
        timeoflastshot += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if (timebeforeshooting <= 0f)
            {
                shoot();
                timebeforeshooting = 1 / firerate;
            }
            else
            {
                timebeforeshooting -= Time.deltaTime;
            }
        }
        else if (Input.GetMouseButtonDown(0) && timeoflastshot >= timebeforeshooting)
        {
            timeoflastshot = 0f;
            shoot();
        }
        else
        {
            timebeforeshooting -= Time.deltaTime;
        }
    }

    private void shoot()
    {
        gunsoundsource.PlayOneShot(revolvershot);

        Ray gunray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(gunray, out RaycastHit hitInfo, 150f, enemylayer))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out EnemyScript enemyhit))
            {
                enemyhit.takedamage(damage);
                print(enemyhit.health);
            }
        }
    }
}
 */