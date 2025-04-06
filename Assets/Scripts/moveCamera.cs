using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public Transform cameraPos;

    void Update()
    {
        transform.position = cameraPos.position; //updates the camera position to move with the player
    }
}
