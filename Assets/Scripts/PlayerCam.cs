using System;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float SensX = 150f;
    public float SensY = 150f;

    public Transform playerObj;

    private float xRotation;
    private float yRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center of screen
        Cursor.visible = false; // Hide cursor
    }

    // Update is called once per frame
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * SensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * SensY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        // Limit vertical look angle
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        // Rotate camera based on mouse movement
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        
        // Rotate player orientation based on mouse movement
        playerObj.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}