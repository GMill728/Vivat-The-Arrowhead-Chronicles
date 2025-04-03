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