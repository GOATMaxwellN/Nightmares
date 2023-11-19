using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraposition;

    private void Update()
    {
        transform.position = cameraposition.position;
    }
}
