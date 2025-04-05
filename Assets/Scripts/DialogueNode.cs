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
    public string id;               //L - used to find node
    public string dialogueText;     //L - stores node text to input into UI container

    public bool hasNewActorLink;    //L - If checked in inspector, other function use linkActor and linkNodeId
    public string linkActor;        //L - Within inspector, the input should equate to other GameObject's tag & actorName
    public string linkNodeId;       //L - Within inspector, the input should equate to other DialogueNode's id

    public List<DialogueResponse> responses;    //L - list of every response within node

    // Optional reference to the dialogue this node belongs to
    // public Dialogue dialogue

    /// <summary>
    /// L - Checks if node contains any responses
    /// </summary>
    /// <returns>bool</returns>
    internal bool hasResponses()
    {
        return responses.Count > 0;     //L - see function summary
    }//END hasResponses()
}//END class DialogueNode