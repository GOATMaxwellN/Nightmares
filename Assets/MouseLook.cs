using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensx;
    public float sensy;
    public Transform orientation;

    float xrot;
    float yrot;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //mouse input
        float mousex = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensx;
        float mousey = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensy;

        yrot += mousex;
        xrot -= mousey;
        xrot = Mathf.Clamp(xrot, -90f, 90f);

        transform.rotation = Quaternion.Euler(xrot, yrot, 0);
        orientation.rotation = Quaternion.Euler(0, yrot, 0);
    }
}
