/*
This script handles all detection related variables and the detection UI
when other scripts increase, decrease, or otherwise change detection, they simply
need to interface with the dStat variable (detection status.)  This script will handle
that and process propper UI and game loss conditions accordnigly.
Written by: Gavv
On: 4/1/25
*/
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class Detection : MonoBehaviour
{

    FieldOfView fieldOfView;
    AudioManager audioManager;

    public GameObject[] allObjects;

    [SerializeField] GameObject nearestEnemy;

    public GameObject player;

    Rigidbody rb;

    float distance; 

    float nearestDistance;

    void getNearestEnemy()
    {
        allObjects = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < allObjects.Length; i++)
        {
            distance = Vector3.Distance(this.transform.position, allObjects[i].transform.position);

            if(distance < nearestDistance)
            {
                nearestEnemy = allObjects[i];
                nearestDistance = distance;
            }
        }
        nearestDistance = Mathf.Infinity;

    }

    void Awake()
    {
        //fieldOfView = enemyObject.GetComponent<FieldOfView>();
        fieldOfView = nearestEnemy.GetComponent<FieldOfView>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        rb = player.GetComponent<Rigidbody>();
    }

    public static bool Game_Over = false;
    //below are where to put detection images
    public Image image_0;
    public Image image_1;
    public Image image_2;
    public Image image_3;
    public Image image_4;
    public Image image_5;
    
    public float timer;
    public float gameOverTimer;

    int dStat = 0;//this is the initial value of detection status
    int maxD = 24;//max detection status
    int minD = 0;//minimum detection status

    bool hasPlayedAudio = false;

    void PlusAlpha(Image b){//function to add alpha (transparency to input image)
        var tempColor = b.color;//creates a temp var of all color channels (you can't just edit em directly)
         tempColor.a += 0.25f;//adds .25 (out of max 1) to alpha since we are doing 4 frames per image
         b.color = tempColor;//assigns this new alpha to the image
    }
    void MinusAlpha(Image b){//does the same thing as above but minus
        var tempColor = b.color;
         tempColor.a -= 0.25f;
         b.color = tempColor;
    }
    void ZeroAlpha(Image b){//does the same thing as above but zeros out the alpha
        var tempColor = b.color;
         tempColor.a = 0;
         b.color = tempColor;
    }
      void MaxAlpha(Image b){//maxes alpha of given image inputted
        var tempColor = b.color;
         tempColor.a = 1;
         b.color = tempColor;
    }
    void addFrame(){//adding frames function description below
    /*
        going 0->23 (24 frames), every 4 frames is a new image with the same operations.
        to do this we just assign which image it is and call the PlusAlpha funciton
        to add alpha to the image, giving the illusion of more frames and a fade in
    */
        if (dStat <= 3){
         PlusAlpha(image_0);
        }
        else if (dStat <= 7){
        MaxAlpha(image_0);
        PlusAlpha(image_1);
        }
        else if (dStat <= 11){
        MaxAlpha(image_1);
        PlusAlpha(image_2);
        }
        else if (dStat <= 15){
        MaxAlpha(image_2);
        PlusAlpha(image_3);
        }
        else if (dStat <= 19){
        MaxAlpha(image_3);
        PlusAlpha(image_4);
        }
        else if (dStat <= 23){
        MaxAlpha(image_4);
        PlusAlpha(image_5);
        }
    }
    void subtractFrame(){//this does the same as above but subtraction
        if (dStat == 0){
            zeroFrame();
        }
        else if (dStat <= 3){
         MinusAlpha(image_0);
         ZeroAlpha(image_1);
        }
        else if (dStat <= 7){
        MinusAlpha(image_1);
        ZeroAlpha(image_2);
        }
        else if (dStat <= 11){
        MinusAlpha(image_2);
        ZeroAlpha(image_3);
        }
        else if (dStat <= 15){
        MinusAlpha(image_3);
        ZeroAlpha(image_4);
        }
        else if (dStat <= 19){
         MinusAlpha(image_4);
         ZeroAlpha(image_5);
        }
        else if (dStat <= 23){
          MinusAlpha(image_5);
        }
    }
    void zeroFrame(){ //this just calls the ZeroAlpha() function which sets the alpha of an image to 0 (transparent)
        ZeroAlpha(image_0);
        ZeroAlpha(image_1);
        ZeroAlpha(image_2);
        ZeroAlpha(image_3);
        ZeroAlpha(image_4);
        ZeroAlpha(image_5);
        }

    void Start()
    {
          updateDetection();//on start check detection once (used for function declaration)
          getNearestEnemy();
    }
    void Update() 
    {
        getNearestEnemy();
        fieldOfView = nearestEnemy.GetComponent<FieldOfView>();
        int dTemp = dStat;
        
        if (Input.GetKeyDown(KeyCode.Alpha0))//if the user hits 0
        {
            dStat = 0;//zero out detection status
            updateDetection();//update detection
            zeroFrame();//zero out UI frame
        }

        if(fieldOfView.playerinFOV) 
        {
            timer -= Time.deltaTime; //Use timer to make sure elements aren't added every frame
            if(timer < 0)
            {
                dStat++; //add one to detection status
                timer = 0.05f; //reset timer
            }
            updateDetection();//run update detection script
            if (dTemp !=dStat)
            {
                addFrame();//add one to the detection UI frames
            }
        } else if(fieldOfView.playerinFOV == false)
        {
            timer -= Time.deltaTime; //Use timer to make sure elements aren't added every frame
            if(timer < 0)
            {
                dStat--;//minus one to detection status
                timer = 0.1f; //reset timer
            }
            updateDetection();//run update detection script
            if (dTemp !=dStat)
            {
                subtractFrame();//minus one to the detection UI frames
            }
        }
    }

    void updateDetection() {//update detection function
        if (dStat == 19){//if the status is greater than max, 
            //dStat = maxD;//set status to max
            Game_Over = true;//set global game over bool to true

            //plays the death audio and prevents it from looping
            if (!hasPlayedAudio)
            {
                audioManager.PlaySFX(audioManager.death, 0.5f);
                hasPlayedAudio = true;
            }
            
        }
        else if (Game_Over)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll; //Freeze when player is caught
            //Added by Luke - trigger guard dialogue when player is caught
            string guardName = GameObject.FindWithTag("Guard").GetComponent<NpcDialogueActor>().ActorName;  //retrive actor name
            string guardDialogueNum = GameObject.FindWithTag("Guard").GetComponent<NpcDialogueActor>().interactDialogueNum; // retrieve actor starting dialogue
            DialogueManager.Instance.linkActorVar = guardName;  //update DialogueManager's temp variables for circumstances requiring these strings
            DialogueManager.Instance.linkNodeIdVar = guardDialogueNum;
            DialogueManager.Instance.SpeakToNewActor(guardName, guardDialogueNum); //Begin dialogue

            
            gameOverTimer -= Time.deltaTime;

            if(gameOverTimer < 0)
            {

                SceneManager.LoadScene("Main Menu");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

        }
        else if (dStat < minD){//if status is less than 0, set it to 0
            dStat = minD;
        }
    }

}