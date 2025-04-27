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

    //Added by Luke to stop player during dialogue
    public bool frozen = false;

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
        if (!frozen)//check if dialogue has stopped movement -Luke 
        {
            MyInput(); //checks inputs every update
            FixedUpdate(); //moves the player every update
            footstepTimer--;

            //if the timer reaches 0 and the player is moving, play the footstep audio
            if (footstepTimer <= 0 && direction.magnitude > 0)
            {
                audioManager.PlaySFX(audioManager.footsteps, 0.15f);
                footstepTimer = 60;
            }
        }
        else
        {

            //stop movement so player doesn't continue walking while frozen - Luke
            direction = (orientation.forward * vertical + orientation.right * horizontal) * 0;

        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction.normalized * speed;
    }
 }

