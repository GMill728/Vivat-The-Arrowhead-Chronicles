/*
This script changes scene on button click from player
Written by: Gavv
On: 3/28/25
*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class sceneClick : MonoBehaviour {
	public Button yourButton;//on given button
    public string sceneName = "Level";//to given level
	
	//both input in Unity engine

	void Start () {
		Button btn = yourButton.GetComponent<Button>();//temp btn is equal to the botton
		btn.onClick.AddListener(TaskOnClick);//adds a listener to do something on click to btn
	}

	void TaskOnClick(){
		SceneManager.LoadScene(sceneName);//when button clicked, change to given scene name
	}
}
