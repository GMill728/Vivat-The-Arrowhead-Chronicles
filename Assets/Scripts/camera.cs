using UnityEngine;

public class camera : MonoBehaviour
{
    public float sensitivity;

    public Transform orientation;

    float xRot;
    float yRot;

    void Start() {
        //locks the cursor and makes it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        //gets the rate that the mouse is moving in the X and Y directions
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;

        //updates the rotation variables
        yRot += mouseX;
        xRot -= mouseY;

        //clamps the x rotation to prevent turning upwards/downwards 360 degrees
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRot, yRot, 0); //rotates the camera
        orientation.rotation = Quaternion.Euler(0, yRot, 0); //rotates the orientation with just the Y component
    }
}
