/*
This script is to output the current detection status for development and debug
Will become depricated before Alpha
Written by: Gavv
On: 3/28/25
*/
using UnityEngine;
using TMPro;
using System.Collections;

public class debugDetection : MonoBehaviour
{
    /*creates a field able to be edited in unity editor
    but is not accessible by other classes*/
    [SerializeField] TextMeshProUGUI myObj;
                                        
    int dStat = 0;//detection status variable init. at 0
    int maxD = 24;//max detection value
    int minD = 0;//min detection value
   
    void Start()
    {
          updateDetection();//set detection 
    }
    void Update() 
    {


        if (Input.GetKeyDown(KeyCode.Equals))//if key "+" key pressed
        {
            dStat++;//increase detection status
            updateDetection();//handles exceptions of max and min and zeroing detection
         
        }
        else if (Input.GetKeyDown(KeyCode.Minus))//if key "-" key pressed
        {
            dStat--;//decrease detection status
            updateDetection();//handles exceptions of max and min and zeroing detection
        
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))//if key "0" key pressed
        {
            dStat = 0;//zero out detection status
            updateDetection();//handles exceptions of max and min and zeroing detection
            
        }

    }
    void updateDetection() {//function to handle detection max and mins as well as text output
        if (dStat > maxD){//if new status is above max value
            dStat = maxD;//set it to the max value again instead
        }
        else if (dStat < minD){//if new status is below 0
            dStat = minD;//set it to zero again
        }
        else if (dStat == 0) {//if detection status is 0
            myObj.text = "Detection Status: NOT DETECTED";//output "NOT DETECTED" text
        }
        else {
            myObj.text = "Detection Status: " + dStat;//otherwise output curent detecion status
        }
    }
}
