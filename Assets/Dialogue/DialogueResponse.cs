// Created by Luke using source code from Youtube 
// channel Dul at https://www.youtube.com/watch?v=dcPIuTS_usM
// and easily accessible code from https://pastebin.com/DgyxWJ5T
// Additional notes and changes included by Luke annotated by: L -

[System.Serializable]
public class DialogueResponse
{
    //L - What the player is saying back
    public string responseText;

    //L - next bit of Dialogue
    // Use an ID to reference the next node instead
    public string nextNodeId;
}