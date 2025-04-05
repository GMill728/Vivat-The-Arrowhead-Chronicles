// Created by Luke using source code from Youtube 
// channel Dul at https://www.youtube.com/watch?v=dcPIuTS_usM
// and easily accessible code from https://pastebin.com/DgyxWJ5T
// Additional notes and changes included by Luke annotated by: L -

using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Asset")]

// [System.Serializable] L - wasn't in troubleshooting steps
public class Dialogue : ScriptableObject
{
    //First node of the conversation
    public DialogueNode RootNode;

    //List of all dialogue nodes
    public List<DialogueNode> dialogueNodes; 
}