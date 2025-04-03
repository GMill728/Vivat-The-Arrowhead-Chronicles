// Created by Luke using source code from Youtube 
// channel Dul at https://www.youtube.com/watch?v=dcPIuTS_usM
// and easily accessible code from https://pastebin.com/DgyxWJ5T
// Additional notes and changes included by Luke annotated by: L -

using System;
using System.Collections.Generic;
using static UnityEngine.Debug;
 
[System.Serializable]
public class DialogueNode
{
    //Unique identifier for the dialogue node
    public string id;
    public string dialogueText;
    public List<DialogueResponse> responses;

    // Optional reference to the dialogue this node belongs to
    // public Dialogue dialogue

    
    internal bool IsLastNode()
    {
        //L - attempted: return dialogueText == "close";
        // successful, but changing it to attempt other methods
        return responses.Count <= 0;
    }


    // L - I made this function to account for nodes with no response.
    // It doesn't work.... Earlier I successfully accomplished it using dialogue
    // as the trigger instead. Comments showing the code are in IsLastNode()
    // This function doesn't seem as relevant now that the node tree in the dialogue
    // performs better(not having responses for a next part simply ends the tree). Unfortunately,
    // I still need to make it so there are no options, but still dialogue you can pass with
    // hitting spacebar similar to the key input in the Actor.cs script.
    internal bool IsStmtNode()
    {
        foreach (DialogueResponse response in responses)
        {
            if (response.responseText == "")
            {
                Log("response empty/true");
                return true;
            }
            
        }
        Log("response full/false");
        return false;
    }
}