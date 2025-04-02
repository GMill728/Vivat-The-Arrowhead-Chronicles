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
    public Image image_0;
    public Image image_1;
    public Image image_2;
    public Image image_3;
    public Image image_4;
    public Image image_5;

    int dStat = 0;
    int maxD = 24;
    int minD = 0;
   
    void Start()
    {
          updateDetection();
    }
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            dStat++; //could pass 3 variables to deteciton to eliminate this line
            updateDetection();
            addFrame();
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            dStat--;
            updateDetection();
            subtractFrame();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            dStat = 0;
            updateDetection();
            zeroFrame();
        }

    }
    void updateDetection() {
        if (dStat > maxD){
            dStat = maxD;
            Game_Over = true;
        }
        else if (dStat < minD){
            dStat = minD;
        }
        else if (dStat == 0) {
            }
    }
    void PlusAlpha(Image b){
        var tempColor = b.color;
         tempColor.a += 0.25f;
         b.color = tempColor;
    }
    void MinusAlpha(Image b){
        var tempColor = b.color;
         tempColor.a -= 0.25f;
         b.color = tempColor;
    }
    void addFrame(){
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
    void subtractFrame(){
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
    void zeroFrame(){
        MinusAlpha(image_0);
        MinusAlpha(image_1);
        MinusAlpha(image_2);
        MinusAlpha(image_3);
        MinusAlpha(image_4);
        MinusAlpha(image_5);
    }
}
