/*
Script handles assigning an actor name and their associated dialogue as well as
which corresponding dialogue node to be used.
Written by: Luke
On: 3/28/25
Last Modified: 4/6/25
*/

// Created by Luke using source code from Youtube 
// channel Dul at https://www.youtube.com/watch?v=dcPIuTS_usM
// and easily accessible code from https://pastebin.com/DgyxWJ5T
// Additional notes and changes included by Luke annotated by: L -

using UnityEngine;
 
public class NpcDialogueActor : MonoBehaviour, IInteractable        //L - Character in scene that you're talking to
{
    //L - Name used for referencing correct GameObject Dialogue
    public string ActorName;

    //L - required dialogue script to be attached to GameObject
    //      (see Dialogue.cs for better breakdown)
    public Dialogue Dialogue;

    public string interactDialogueNum; //L - used to determine which dialogue node to start with when interacted.
                                        // needs to be string for cases like 1a and 1b.
                                        // Can be changed by other script (DialogueEventTracker) in order to change the dialogue that appears
                                        // when various events occur in game.

    /// <summary>
    /// Trigger dialogue for this actor | L - NOT TYPICALLY USED. SpeakToNewActor() within DialogueManager has wider use.
    /// </summary>
    /// <param name="ActorName">This actor's name</param>
    /// <param name="node">A node within the list of nodes in current dialogue.</param>
    public void SpeakTo(string ActorName, DialogueNode node)
    {
        //requiring already having the node restricts how much this can be used. Likely redundant.
        DialogueManager.Instance.StartDialogue(ActorName, node);
    }//END SpeakTo

    public void Interact()
    {
        DialogueManager.Instance.linkActorVar = ActorName;  //update DialogueManager's temp variables for circumstances requiring these strings
        DialogueManager.Instance.linkNodeIdVar = interactDialogueNum;
        DialogueManager.Instance.SpeakToNewActor(ActorName, interactDialogueNum); //Begin dialogue
    }

    public string InteractableInfo()
    {
        return ActorName;
    }
}//END class NpcDialogueActor