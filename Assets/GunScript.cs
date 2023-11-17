using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    public GameObject gun;

    private Camera cam;
    [SerializeField] private AudioSource gunsoundsource;
    [SerializeField] private AudioSource reloadsource;
    [SerializeField] private AudioClip revolvershot;
    [SerializeField] private AudioClip revolverreload;
    [SerializeField] private ParticleSystem muzzleflash;

    [SerializeField] private LayerMask enemylayer;
    [SerializeField] private float damage;
    [SerializeField] private float firerate;
    [SerializeField] private int chamber;
    private int bullets;
    [SerializeField] private float reloadtime = 1f;
    private bool reloading = false;

    private float timebeforeshooting;
    private float timeoflastshot = 0f;

    public Transform camholder;
    public AnimationCurve sscurve;
    public float ssduration = 1f;

    [SerializeField] private UnityEngine.UI.Image crosshair;

    void Start()
    {
        bullets = chamber;
        timebeforeshooting = 1 / firerate;
        cam = Camera.main;
    }

    void Update()
    {
        timeoflastshot += Time.deltaTime;

        if (reloading) return;

        if (bullets <= 0 )
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButton(0))
        {
            if (timebeforeshooting <= 0f)
            {
                Shoot();
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
            Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.R) && bullets<chamber)
        {
            StartCoroutine(Reload());
            return;
        }
        else
        {
            timebeforeshooting -= Time.deltaTime;
        }
    }

    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadtime*2/3);
        reloadsource.PlayOneShot(revolverreload);
        yield return new WaitForSeconds(reloadtime/3);
        bullets = chamber;
        reloading = false;
    }

    private void Shoot()
    {
        bullets--;
        muzzleflash.Play();
        StartCoroutine(StartRecoil());
        StartCoroutine(ScreenShake());
        StartCoroutine(CursorExpand());
        gunsoundsource.PlayOneShot(revolvershot);

        Ray gunray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(gunray, out RaycastHit hitInfo, 150f, enemylayer))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out EnemyScript enemyhit))
            {
                enemyhit.takedamage(damage);
                print("HIT");
            }
        }
    }

    IEnumerator StartRecoil()
    {
        gun.GetComponent<Animator>().Play("Recoil");
        yield return new WaitForSeconds((1 / firerate)*0.9f);
        gun.GetComponent<Animator>().Play("New State");
    }

    IEnumerator ScreenShake()
    {
        float elapsedtime = 0f;

        while (elapsedtime < ssduration)
        {
            elapsedtime += Time.deltaTime;
            float strength = sscurve.Evaluate(elapsedtime/ssduration);
            cam.transform.position = camholder.position + Random.insideUnitSphere * strength;
            yield return null;
        }
    }

    IEnumerator CursorExpand()
    {
        float elapsedtime = 0f;
        Vector2 initialsize = crosshair.rectTransform.sizeDelta;

        while (elapsedtime < ssduration/3)
        {
            elapsedtime += Time.deltaTime;
            float strength = sscurve.Evaluate(elapsedtime / ssduration)*50f;
            crosshair.rectTransform.sizeDelta = initialsize + new Vector2(strength, strength);
            yield return null;
        }

        crosshair.rectTransform.sizeDelta = initialsize;
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