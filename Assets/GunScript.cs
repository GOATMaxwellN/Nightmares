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
    [SerializeField] private float reloadtime = 1f;
    private bool reloading = false;
    public UnityEngine.UI.Image reloadicon;

    [SerializeField] private int chamber;
    public int bullet;
    public UnityEngine.UI.Image[] bullets;


    private float timebeforeshooting;
    private float timeoflastshot = 0f;

    public Transform camholder;
    public AnimationCurve sscurve;
    public float ssduration = 1f;

    [SerializeField] private UnityEngine.UI.Image crosshair;

    void Start()
    {
        bullet = chamber;
        timebeforeshooting = 1 / firerate;
        cam = Camera.main;
        reloadicon.color = new Color(0.7f, 0, 0.15f);
        reloadicon.enabled = false;
    }

    void Update()
    {
        timeoflastshot += Time.deltaTime;

        if (reloading) return;

        if (bullet <= 0 )
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
        else if (Input.GetKeyDown(KeyCode.R) && bullet<chamber)
        {
            StartCoroutine(Reload());
            return;
        }
        else
        {
            timebeforeshooting -= Time.deltaTime;
        }

        // UI update
        for (int i = 0; i < bullets.Length; i++)
        {
            if (i < bullet) { bullets[i].color = new Color(0.7f, 0, 0.15f); }
            else { bullets[i].color = new Color(0, 0, 0); }

        }
        if (bullet != bullets.Length && reloading == false) { reloadicon.enabled = true; }
    }

    IEnumerator Reload()
    {
        reloading = true;
        reloadicon.enabled = false;
        yield return new WaitForSeconds(reloadtime/2);
        gun.GetComponent<Animator>().Play("Reload");
        reloadsource.PlayOneShot(revolverreload);
        yield return new WaitForSeconds(reloadtime/2);
        gun.GetComponent<Animator>().Play("New State");
        bullet = chamber;
        reloading = false;
    }

    private void Shoot()
    {
        bullet--;
        muzzleflash.Play();
        StartCoroutine(StartRecoil());
        StartCoroutine(ScreenShake());
        StartCoroutine(CursorExpand());
        gunsoundsource.PlayOneShot(revolvershot);

        Ray gunray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(gunray, out RaycastHit hitInfo, 150f, enemylayer))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out EnemyHealth enemyhit))
            {
                enemyhit.takedamage(damage);
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