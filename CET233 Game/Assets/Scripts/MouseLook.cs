using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sens = 100f;
    public Transform player;

    float xRot = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
        
    void Update()
    {
        Look();  
    }

    void Look()
    {
        float mX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        xRot -= mY;
        xRot = Mathf.Clamp(xRot, -75f, 75f);

        player.Rotate(Vector3.up * mX);
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);       

    }
}
