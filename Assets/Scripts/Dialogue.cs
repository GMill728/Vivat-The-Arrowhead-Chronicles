/*
Script handles assigning and storing information for the various nodes of dialogue that
are later used to fill dialogueUI. See Assets>Dialogue>Dialogue.asset
Written by: Luke
On: 3/28/25
Last Modified: 4/6/25
*/

// Created by Luke using source code from Youtube 
// channel Dul at https://www.youtube.com/watch?v=dcPIuTS_usM
// and easily accessible code from https://pastebin.com/DgyxWJ5T
// Additional notes and changes included by Luke annotated by: L -

using System.Collections.Generic;
using UnityEngine;
 
 // L - creates a new asset when right-clicking     create > dialogue > dialogue asset
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Asset")]

// [System.Serializable] L - removed to fix loop warning.
public class Dialogue : ScriptableObject
{
    //First node of the conversation
    // public DialogueNode RootNode;    L - thought about switching to string for a search function but seems unecessary for now. 
    //                                  will likely include/change when determining and changing dialogue upon approach to npc/actor.

    //List of all dialogue nodes
    public List<DialogueNode> dialogueNodes; 
}//END class Dialogue