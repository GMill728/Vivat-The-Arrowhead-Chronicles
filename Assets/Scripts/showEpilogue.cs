using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class showEpilogue : MonoBehaviour
{

    public float timeDelay;
    bool hasPlayedSound = false;
    public AudioSource audioData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (DialogueEventTracker.optionB)
        {
            DialogueManager.Instance.linkActorVar = "EpilDialObj";  //update DialogueManager's temp variables for circumstances requiring these strings
            DialogueManager.Instance.linkNodeIdVar = "1";
            DialogueManager.Instance.SpeakToNewActor("EpilDialObj", "1");
        }
        else
        {
            DialogueManager.Instance.linkActorVar = "EpilDialObj";  //update DialogueManager's temp variables for circumstances requiring these strings
            DialogueManager.Instance.linkNodeIdVar = "5";
            DialogueManager.Instance.SpeakToNewActor("EpilDialObj", "5");
        }
    }

    void Update()
    {
        if (!DialogueManager.Instance.IsDialogueActive())
        {
            for(float i = 0; i < timeDelay; i += Time.deltaTime) // create delay after dialogue closes
            {}
             if(!audioData.isPlaying && hasPlayedSound == false){ //Make sure win sound doesn't play over itself, or repeat
                audioData.Play();
                hasPlayedSound = true; //Make sure sound ONLY plays ONCE
                }
            
            SceneManager.LoadScene("Main Menu");
            Cursor.lockState = CursorLockMode.None; //unlock cursor
            Cursor.visible = true;
        }
    }
}
