// Created by Luke using source code from Youtube 
// channel Dul at https://www.youtube.com/watch?v=dcPIuTS_usM
// and easily accessible code from https://pastebin.com/DgyxWJ5T
// Additional notes and changes included by Luke annotated by: L -

using UnityEngine;
 
public class NpcDialogueActor : MonoBehaviour        //L - Character in scene that you're talking to
{
    public string ActorName;
    public Dialogue Dialogue;


    //L - used for testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SpeakTo();
        }
    }

    // Trigger dialogue for this actor
    public void SpeakTo()
    {
            DialogueManager.Instance.StartDialogue(ActorName, Dialogue.RootNode);
    }
}