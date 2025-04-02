using UnityEngine;
using TMPro;
using System.Collections;

public class debugDetection : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myObj;
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
            dStat++;
            updateDetection();
         
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            dStat--;
            updateDetection();
        
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            dStat = 0;
            updateDetection();
            
        }

    }
    void updateDetection() {
        if (dStat > maxD){
            dStat = maxD;
            //!GAME OVER
        }
        else if (dStat < minD){
            dStat = minD;
        }
        else if (dStat == 0) {
            myObj.text = "Detection Status: NOT DETECTED";
        }
        else {
            myObj.text = "Detection Status: " + dStat;
        }
    }
}
