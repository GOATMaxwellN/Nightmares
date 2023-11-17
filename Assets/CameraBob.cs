using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour
{
    public float _Amplitude = 0.0015f;
    public float _frequency = 10.0f;
    public Transform cam;
    public Transform cameraHolder;

    private float _toggleSpeed = 2.0f;
    private Vector3 _startPos;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _startPos = cam.localPosition;
    }

    void Update()
    {
        CheckMotion();
        ResetPosition();
        cam.LookAt(FocusTarget());
    }
    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency) * _Amplitude * 1.5f; //vert
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _Amplitude; //hori
        return pos;
    }
    private Vector3 StandMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency/4.2f) * _Amplitude * 0.25f; //vert
        return pos;
    }
    private void CheckMotion()
    {
        float speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        if (speed < _toggleSpeed) PlayMotion(StandMotion()); else PlayMotion(FootStepMotion());
    }
    private void PlayMotion(Vector3 motion)
    {
        cam.localPosition += motion;
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolder.localPosition.y, transform.position.z);
        pos += cameraHolder.forward * 15.0f;
        return pos;
    }
    private void ResetPosition()
    {
        if (cam.localPosition == _startPos) return;
        cam.localPosition = Vector3.Lerp(cam.localPosition, _startPos, 1 * Time.deltaTime);
    }
}