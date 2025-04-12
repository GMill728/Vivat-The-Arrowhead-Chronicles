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

public class Detection : MonoBehaviour
{
    public static bool Game_Over = false;
    //below are where to put detection images
    public Image image_0;
    public Image image_1;
    public Image image_2;
    public Image image_3;
    public Image image_4;
    public Image image_5;

    int dStat = 0;//this is the initial value of detection status
    int maxD = 24;//max detection status
    int minD = 0;//minimum detection status

    bool hasPlayedAudio = false;

    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
          updateDetection();//on start check detection once (used for function declaration)
    }
    void Update() 
    {
        int dTemp = dStat;
        
        if (Input.GetKeyDown(KeyCode.Equals))//check if plus was pressed (equals key)
        {
            dStat++; //add one to detection status
            updateDetection();//run update detection script
            if (dTemp !=dStat)
            {
                addFrame();//add one to the detection UI frames
            }
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            dStat--;//minus one to detection status
            updateDetection();//run update detection script
            if (dTemp !=dStat)
            {
                subtractFrame();//minus one to the detection UI frames
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))//if the user hits 0
        {
            dStat = 0;//zero out detection status
            updateDetection();//update detection
            zeroFrame();//zero out UI frame
        }

    }
    void updateDetection() {//update detection function
        if (dStat > maxD){//if the status is greater than max, 
            dStat = maxD;//set status to max
            Game_Over = true;//set global game over bool to true
            
            if (!hasPlayedAudio) {
                audioManager.PlaySFX(audioManager.death);
                hasPlayedAudio = true;
            }
        }
        else if (dStat < minD){//if status is less than 0, set it to 0
            dStat = minD;
        }
    }
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
        PlusAlpha(image_1);
        }
        else if (dStat <= 11){
        PlusAlpha(image_2);
        }
        else if (dStat <= 15){
        PlusAlpha(image_3);
        }
        else if (dStat <= 19){
         PlusAlpha(image_4);
        }
        else if (dStat <= 23){
          PlusAlpha(image_5);
        }
    }
    void subtractFrame(){//this does the same as above but subtraction
        if (dStat <= 3){
         MinusAlpha(image_0);
        }
        else if (dStat <= 7){
        MinusAlpha(image_1);
        }
        else if (dStat <= 11){
        MinusAlpha(image_2);
        }
        else if (dStat <= 15){
        MinusAlpha(image_3);
        }
        else if (dStat <= 19){
         MinusAlpha(image_4);
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
}
