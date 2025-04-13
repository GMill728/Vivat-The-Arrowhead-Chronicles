using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerMovement : MonoBehaviour
{

    Rigidbody rb;
    float speed = 5f;
    Vector3 direction;
    public Transform orientation;

    float horizontal;
    float vertical;

    int footstepTimer = 60;

    AudioManager audioManager;

    private void Awake()
    {
        //accesses the audio manager
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void MyInput()
    {
        //checks for horizontal and vertical inputs
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = orientation.forward * vertical + orientation.right * horizontal;
    }

    void Start(){
 	    rb = GetComponent<Rigidbody>();
	}
    void Update()
    {
        MyInput(); //checks inputs every update
        FixedUpdate(); //moves the player every update
        footstepTimer--;

        //if the timer reaches 0 and the player is moving, play the footstep audio
        if (footstepTimer <= 0 && direction.magnitude > 0)
        {
            audioManager.PlaySFX(audioManager.footsteps, 0.25f);
            footstepTimer = 60;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction.normalized * speed;
    }
 }

