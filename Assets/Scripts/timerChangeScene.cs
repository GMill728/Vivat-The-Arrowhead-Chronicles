/*
This script handles changing scene automatically 
after a given time and initial trigger of script
(used for changing scenes automatically after cutscenes)
Written by: Gavv
On: 3/28/25
*/
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SimpleTimer: MonoBehaviour {

    public float targetTime = 60.0f;//can edit time in Unity engine
    public string sceneName = "Level";//can change scene name (default is Level)

    void Update(){

        targetTime -= Time.deltaTime;//timer to tick down per second from input time

        if (targetTime <= 0.0f || Input.GetKeyDown("space"))//if timer is at 0
        {
           timerEnded();//end timer (alarm in other engines)
        }



    }

    void timerEnded()//on timer end (alarm)
    {
        SceneManager.LoadScene(sceneName);//call SceneManager and load the input scenename
    }
}