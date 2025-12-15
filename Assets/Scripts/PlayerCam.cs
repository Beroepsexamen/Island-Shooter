using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float SensX = 150f;
    public float SensY = 150f;

    public Transform PlayerObj;

    float XRotation;
    float YRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center of screen
        Cursor.visible = false; // Hide cursor
    }

    // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * SensX * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * SensY * Time.deltaTime;

        YRotation += MouseX;
        XRotation -= MouseY;

        XRotation = Mathf.Clamp(XRotation, -90f, 90f); // Limit vertical look angle

        transform.rotation = Quaternion.Euler(XRotation, YRotation, 0f); // Rotate camera based on mouse movement
        PlayerObj.rotation = Quaternion.Euler(0f, YRotation, 0f); // Rotate player orientation based on mouse movement
    }
}