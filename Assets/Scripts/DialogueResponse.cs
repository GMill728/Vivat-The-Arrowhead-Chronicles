/*
Script handles assigning and storing information for the various responses in nodes of dialogue that
are later used to fill dialogueUI. See Assets>Dialogue>Dialogue.asset
Written by: Luke
On: 3/28/25
Last Modified: 4/6/25
*/

// Created by Luke using source code from Youtube 
// channel Dul at https://www.youtube.com/watch?v=dcPIuTS_usM
// and easily accessible code from https://pastebin.com/DgyxWJ5T
// Additional notes and changes included by Luke annotated by: L -

[System.Serializable]
public class DialogueResponse
{
    //L - What the player is saying back
    public string responseText;

    //L - next bit of Dialogue      [!! Although it was described as next bit of dialogue, it appears to still be current dialogue for node. This is not to say it functions incorrectly. !!]
    // Use an ID to reference the next node instead
    public string responseId;

    public bool hasNewActorLink;    //L - If checked in inspector, other function use linkActor and linkNodeId
    public string linkActor;        //L - Within inspector, the input should equate to other GameObject's tag & actorName
    public string linkNodeId;       //L - Within inspector, the input should equate to other DialogueNode's id
}//END class DialogueResponse