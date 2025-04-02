  using UnityEngine;
  using UnityEngine.UI;
  using System.Collections;
  using System;
using UnityEngine.Rendering;
public class alphaTest : MonoBehaviour{


  public Image image;
  public float alpha;
  int maxAlpha, minAlpha;

   void Start () 
   {    
         
         image = GetComponent<Image>();
         var tempColor = image.color;
         tempColor.a = alpha;
         image.color = tempColor;

         void plusAlpha(float a){
          alpha += a;
         }
         void minusAlpha(float a){
          alpha -= a;
         }
   }
}