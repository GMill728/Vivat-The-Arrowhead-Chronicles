//Created by Luke
//L - just getting started

using UnityEngine;

public class interactPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DialogueManager.Instance.linkActorVar = "Rebel1";
            DialogueManager.Instance.linkNodeIdVar = "R1";
            DialogueManager.Instance.SpeakToNewActor("Rebel1", "1");
        }//END IF
    }
}
