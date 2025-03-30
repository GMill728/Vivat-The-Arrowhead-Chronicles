// Created by Luke using source code from Youtube 
// channel Dul at https://www.youtube.com/watch?v=dcPIuTS_usM
// and easily accessible code from https://pastebin.com/DgyxWJ5T
// Additional notes and changes included by Luke annotated by: L -

using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
 
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    NpcDialogueActor npcActor;
 
    // UI references
    public GameObject DialogueParent; // Main container for dialogue UI
    public TextMeshProUGUI DialogueTitleText, DialogueBodyText; // Text components for title and body
    public GameObject responseButtonPrefab; // Prefab for generating response buttons
    public Transform responseButtonContainer; // Container to hold response buttons


    // Reference to the current dialogue asset
    public Dialogue currentDialogue; 

    public bool selfdestruct = false;
 
    private void Awake()
    {
        //Added in troubleshooting overhaul
        npcActor = GetComponent<NpcDialogueActor>();
        
        // Singleton pattern to ensure only one instance of DialogueManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
 
        // Initially hide the dialogue UI
        HideDialogue();

    }

    // L - Added to end the dialogue when statement dialogue is complete. Note: Don't think it actually
    //  removes dialogue, just hides it. Will possibly work on it later to inspect.
    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space) && selfdestruct != false)
        {
            HideDialogue();
            selfdestruct = false;
        }
    }
 
    // Starts the dialogue with given title and dialogue node
    public void StartDialogue(string actorName, DialogueNode node)
    {
        // Display the dialogue UI
        ShowDialogue();

        //Log to check Debug.Log($"StartDialogue - Actor: {actorName}, Dialogue Text {node.dialogueText}");
 
        // Set dialogue title and body text
        DialogueTitleText.text = actorName;
        DialogueBodyText.text = node.dialogueText;

        if (!node.IsStmtNode()) //Doesn't work correctly
        {
            CreateResponseButtons(node.responses);
        }
        else
        {
            selfdestruct = true; //L - Intended to allow hide dialogue when pressing space using other code.
        }

        // L - Quick brainstorming notes I threw down:
        //if node id doesnt exist
        //      check if gameobject
        //          if gameobject, use that ones id
        // GameObject Rebel1 = GameObject.FindWithTag("Rebel1") 
        // rebel1.dialogue.dialogueNodes
    }
 
    public void UpdateDialogue(string actorName, DialogueNode node)
    {
        // Log to verify
        Debug.Log($"UpdateDialogue - Actor: {actorName}, Dialogue Text: {node.dialogueText}");

        DialogueTitleText.text = actorName;  // Actor's name in title
        DialogueBodyText.text = node.dialogueText;  // Dialogue text in body

        // Clear previous response buttons
        foreach (Transform child in responseButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // Create new response buttons for the current node's responses
        foreach (DialogueResponse response in node.responses)
        {
            GameObject buttonObj = Instantiate(responseButtonPrefab, responseButtonContainer);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = response.responseText;
            buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectResponse(response));
        }
    }


    // Handles response selection and triggers next dialogue node
    public void SelectResponse(DialogueResponse response)
    {
        // Retrieve the next dialogue node by the ID
        DialogueNode nextNode = GetDialogueNodeById(response.nextNodeId);

        // Ensure we have a valid next node
        if (nextNode != null)
        {
            // Use the correct actor's name (do not overwrite it with dialogue text)
            string actorName = DialogueTitleText.text;  // Preserve the actor's name from the title

            // Log for debugging
            Debug.Log($"SelectResponse - Actor: {actorName}, Next Dialogue Text: {nextNode.dialogueText}");

            // Update the dialogue using the same actor name, but with the new dialogue text for the next node
            UpdateDialogue(actorName, nextNode);  // Pass actor name + updated dialogue text
        }
        else
        {
            HideDialogue();
        }
    }




 
    // Hide the dialogue UI
    public void HideDialogue()
    {
        DialogueParent.SetActive(false);
    }
 
    // Show the dialogue UI
    private void ShowDialogue()
    {
        DialogueParent.SetActive(true);
    }
 
    // Check if dialogue is currently active
    public bool IsDialogueActive()
    {
        return DialogueParent.activeSelf;
    }

     private DialogueNode GetDialogueNodeById(string id)
    {
        // Assuming all nodes are stored in the current dialogue
        return currentDialogue.dialogueNodes.FirstOrDefault(node => node.id == id);
    }

    private void CreateResponseButtons(List<DialogueResponse> responses)
    {
        // Clear any previous buttons
        foreach (Transform child in responseButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // Create a button for each response in the current node
        foreach (DialogueResponse response in responses)
        {
            GameObject buttonObj = Instantiate(responseButtonPrefab, responseButtonContainer);

            // Set the response text on the button
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = response.responseText;

            // When the button is clicked, handle the response selection
            buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectResponse(response));
        }
    }
}