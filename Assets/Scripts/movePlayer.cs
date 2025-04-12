using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 direction;

    int footstepTimer = 60;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void MyInput()
    {
        //checks for horizontal and vertical inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    void Update()
    {
        MyInput(); //checks inputs every update
        MovePlayer(); //moves the player every update

        footstepTimer--;

        if (footstepTimer == 0) {
            if (direction.magnitude > 0) {
                audioManager.PlaySFX(audioManager.footsteps, 0.25f);
            }

            footstepTimer = 60;
        }
    }

    private void MovePlayer()
    {
        direction = orientation.forward * verticalInput + orientation.right * horizontalInput; //updates the direction vector to account for inputs and current orientation

        transform.Translate(direction * moveSpeed * Time.deltaTime); //moves the player in the direction vector at a rate of moveSpeed per frame
    }
}
