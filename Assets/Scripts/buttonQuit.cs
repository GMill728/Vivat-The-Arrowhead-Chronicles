/*
This script makes the quit button quit game and close program
Written by: Gavv
On: 3/26/25
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuitClick : MonoBehaviour {
	public Button urButton;

	void Strt () {
		Button btn = urButton.GetComponent<Button>();
		btn.onClick.AddListener(TskOnClck);
	}

	void TskOnClck(){
		Application.Quit();
	}
}