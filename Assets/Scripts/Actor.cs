// Created by Luke using source code from Youtube 
// channel Dul at https://www.youtube.com/watch?v=dcPIuTS_usM
// and easily accessible code from https://pastebin.com/DgyxWJ5T
// Additional notes and changes included by Luke annotated by: L -

using UnityEngine;
 
public class NpcDialogueActor : MonoBehaviour        //L - Character in scene that you're talking to
{
    //L - Name used for referencing correct GameObject Dialogue
    public string ActorName;

    //L - required dialogue script to be attached to GameObject
    //      (see Dialogue.cs for better breakdown)
    public Dialogue Dialogue;

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
}//END class NpcDialogueActor