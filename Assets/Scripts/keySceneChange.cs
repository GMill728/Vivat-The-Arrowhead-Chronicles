/*
This script changes scene on button click from player
Written by: Gavv
On: 3/28/25
*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class keySceneChange : MonoBehaviour {
    public string sceneName = "Level";//to given level

    void Update()
    {
		if(Input.GetKeyDown(KeyCode.RightControl)){//if RightControl Pressed
			SceneManager.LoadScene(sceneName);//call SceneManager and load the input scenename
      Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
		}
    }

}





