/*
Script handles all dialogue manipulation between possible various actors and their dialogue nodes.
Written by: Luke
On: 3/28/25
Last Modified: 4/12/25
*/

// Created by Luke using source code from Youtube 
// channel Dul at https://www.youtube.com/watch?v=dcPIuTS_usM
// and easily accessible code from https://pastebin.com/DgyxWJ5T
// Additional notes and changes included by Luke annotated by: L -

using System.Linq;                  // L - allows for FirstOrDefault()
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;           // L - allows for events I attempted to use. Likely unnecessary now.
using System.Collections.Generic;
 
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    //Set in functions to be assigned to the correct title/dialogue     (see Actor.cs)
    NpcDialogueActor npcActor;
 
    // UI references
    public GameObject DialogueParent; // Main container for dialogue UI
    public TextMeshProUGUI DialogueTitleText, DialogueBodyText; // Text components for title and body
    public GameObject responseButtonPrefab; // Prefab for generating response buttons
    public Transform responseButtonContainer; // Container to hold response buttons


    // Reference to the current dialogue asset
    public Dialogue currentDialogue; 

    [HideInInspector] public bool linkable = false;     //L - Determine the access to linking to another actor.
    [HideInInspector] public bool skippable = false;    //L - Determine the access to hiding the dialogue to skip the current box
    [HideInInspector] public bool emptyResponse = false;
    [HideInInspector] public string linkActorVar;       //L - Represents the name of the new actor being linked to
    [HideInInspector] public string linkNodeIdVar;      //L - Represents the new node id of the new actor dialogue being linked to
    [HideInInspector] public string currentDialogueText;//L - Represents the most updated dialogue text to be applied when called
    [HideInInspector] public string responseIdVar;
    [HideInInspector] public string responseActorVar;

    [HideInInspector] public string activeId;
 


    /// <summary>
    /// L - Upon creation, ensure only 1 exists, then hide dialogue
    /// </summary>
    private void Awake()
    {
        // Singleton pattern to ensure only one instance of DialogueManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }//END IF
 
        // Initially hide the dialogue UI
        HideDialogue();

    }//END Awake()



    /// <summary>
    /// Each frame, check for input to either change actor dialogue or hide dialogue
    /// </summary>
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && linkable != false)
        {
            //L - disables repeat calls until appropriate
            linkable = false;
            //L - Change actor dialogue to most updated actor and node id (BOTH ARE STRINGS)
            SpeakToNewActor(linkActorVar, linkNodeIdVar);
            ////Debug.Log($" DialogueActive? {IsDialogueActive()}");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && skippable != false)
        {
            //L - disables repeat calls until appropriate
            skippable = false;
            HideDialogue();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && emptyResponse != false)
        {
            //L - disables repeat calls until appropriate
            emptyResponse = false;
            ////Debug.Log($"actor: {responseActorVar} responseId: {responseIdVar}");
            SpeakToNewActor(responseActorVar, responseIdVar);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && (skippable == false && linkable == false))
        {
            ////Debug.Log("Space and null");
        }//END IF


        //L - HERE FOR TESTING. THIS IS HOW FUNCTION WILL BE CALLED BY PLAYER (Replace "this." to appropriate instance)
        // if (Input.GetKeyDown(KeyCode.Return))
        // {
        //     linkActorVar = "Rebel1";
        //     linkNodeIdVar = "R1";
        //     this.SpeakToNewActor("Rebel1", "1");
        // }//END IF
    }//END  Update()
 


    /// <summary>
    /// Starts the dialogue with given title and dialogue node
    /// </summary>
    /// <param name="actorName"></param>
    /// <param name="node">One node from list of nodes in DialogueNode.cs (For starting dialogue where node is variable/unknown, use SpeakToNewActor()</param>
    public void StartDialogue(string actorName, DialogueNode node)
    {
        // Display the dialogue UI
        ShowDialogue();

        //L - Free cursor and lock player & camera
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameObject.FindWithTag("MainCamera").GetComponent<camera>().frozen = true;

        // Set dialogue title and body text
        DialogueTitleText.text = actorName;

        activeId = node.id;

        //L - Added check for when instance has not been set
        if (node.dialogueText != null)
        {
            DialogueBodyText.text = node.dialogueText;
        }
        else
        {
            //L - if not set, the correct text will have been updated in variable to accomodate.
            DialogueBodyText.text = currentDialogueText;
        }//END IF

        //L - For better understanding of these "if" statements, use inspector tab to view dialogue assets
        if (node.hasNewActorLink && node.hasResponses())
        {
            linkActorVar = node.linkActor;      //L - The node applied to function has its string variable actor stored to search by tag later
            linkNodeIdVar = node.linkNodeId;    //L - The node applied to function has its string variable id stored to search by id later
            linkable = true;    //L - allows "Spacebar" to call a new dialogue node, even if its from another actor
        }
        else if (!node.hasNewActorLink && !node.hasResponses())
        {
            //L - allows "Spacebar" to skip dialogue if it doesn't link to another or requires player response
            //      Another way to understand this: This essentially says the current node is just a text node with no input
            skippable = true;
            //Debug.Log($"skippable 1");
        }
        else if (!node.hasNewActorLink && node.hasResponses() && node.responses[0].responseText != "" && node.responses[0] != null)
        {
            ////Debug.Log("array 1");
            //L - Create a button for player for each possible response in the node input into StartDialogue()
            CreateResponseButtons(node.responses);
        }
        else if (!node.hasNewActorLink && node.hasResponses() && node.responses[0].responseText == "")
        {
            ////Debug.Log("array 2");
            //go to response after pressing space
            responseIdVar = node.responses[0].responseId;
            responseActorVar = node.responses[0].linkActor;
            emptyResponse = true;
            //Debug.Log($"empty true 1");
        }        
        else
        {
            //skippable = true;
            ////Debug.Log("Start Dial: No Nodes applied");
            //HideDialogue();   //L - leaving this here for possible testing in the future
        }//END IF
    }//END StartDialogue()
 
    /// <summary>
    /// L - Clears previous text for body and title, then inputs argument variables as new text.
    ///      Also checks for links and responses to use "Spacebar" in Update()
    /// </summary>
    /// <param name="actorName">Name of actor in Actor.cs</param>
    /// <param name="node">One node from list of nodes in DialogueNode.cs </param>
    public void UpdateDialogue(string actorName, DialogueNode node)
    {
        DialogueTitleText.text = actorName;  // Actor's name in title
        DialogueBodyText.text = node.dialogueText;  // Dialogue text in body

        // Clear previous response buttons
        foreach (Transform child in responseButtonContainer)
        {
            Destroy(child.gameObject);
        }//END FOREACH

        if (node.hasResponses())
        {
            skippable = false;
            //Debug.Log("UpdateDialogue(): skippable off");
        // Create new response buttons for the current node's responses
            foreach (DialogueResponse response in node.responses)
            {
                if (response.responseText != "" && response.responseText != null)
                {
                    GameObject buttonObj = Instantiate(responseButtonPrefab, responseButtonContainer);      //L - Create button objects using prefab
                    buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = response.responseText;       //L - Change created button's text
                    buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectResponse(response));   //L - Wait for player's click on button
                }
                else if (response.responseText == "")
                {
                    //Debug.Log("array 3");
                    responseIdVar = node.responses[0].responseId;
                    responseActorVar = node.responses[0].linkActor;
                    emptyResponse = true;
                    //Debug.Log($"empty true 2");
                }
                else //linkable else if statement unnecessary since UpdateDialogue is never passed to a node with link and responses
                {
                    skippable = true;
                    //Debug.Log($"skippable 2");
                }
            }//END FOREACH
        }
        else //linkable else if statement unnecessary since UpdateDialogue is never passed to a node with link and responses
        {
            skippable = true;
            //Debug.Log($"skippable 3");
        }

        //L - THE FOLLOWING IF STATEMENTS ARE ALMOST ENTIRELY REDUNDANT DUE TO THEM BEING SIMILAR TO StartDialogue()'s IF STATEMENTS.
        //      POSSIBLY TURN THEM INTO THEIR OWN FUNCTION.
        // if (node.responses[0].responseText != null)
        // {
        //     //Debug.Log("array 4");
        //     //Debug.Log($"responseText: {node.responses[0].responseText}");
        // }
        //L - For better understanding of these "if" statements, use inspector tab to view dialogue assets
        if (node.hasNewActorLink && node.hasResponses())//L - THIS IS INCREDIBLY UNLIKELY AND PROBABLY USELESS
        {
            linkActorVar = node.linkActor;      //L - The node applied to function has its string variable actor stored to search by tag later
            linkNodeIdVar = node.linkNodeId;    //L - The node applied to function has its string variable id stored to search by id later
            linkable = true;        //L - allows "Spacebar" to call a new dialogue node, even if its from another actor
        }
        else if (!node.hasNewActorLink && node.hasResponses() && node.responses[0].responseText == "")
        {
            //Debug.Log("array 5");
            //go to response after pressing space
            responseIdVar = node.responses[0].responseId;
            responseActorVar = node.responses[0].linkActor;
            emptyResponse = true;
            //Debug.Log($"empty true 3");
        }        
        else if (node.hasNewActorLink && !node.hasResponses())
        {
            linkActorVar = node.linkActor;      //L - The node applied to function has its string variable actor stored to search by tag later
            linkNodeIdVar = node.linkNodeId;    //L - The node applied to function has its string variable id stored to search by id later
            linkable = true;        //L - allows "Spacebar" to call a new dialogue node, even if its from another actor
        }
        else if (!node.hasNewActorLink && !node.hasResponses())
        {
            //L - allows "Spacebar" to skip dialogue if it doesn't link to another or requires player response
            //      Another way to understand this: This essentially says the current node is just a text node with no input
            skippable = true;
            //Debug.Log($"skippable 4");
        }
        else
        {
            ////Debug.Log($"UpdateDialogue: No Nodes applied");
        }//END IF

        activeId = node.id;
    }//END UpdateDialogue()


    /// <summary>
    /// Handles response selection and triggers next dialogue node
    /// </summary>
    /// <param name="response">DialogueResponse</param>
    public void SelectResponse(DialogueResponse response)
    {
        //Ensure response isn't a dead end like "Leave"
        if (response.hasNewActorLink == false && response.responseId != null && response.responseId != "")
        {
            // Retrieve the next dialogue node by the ID
            DialogueNode nextResponse = GetDialogueNodeById(response.responseId);

            // Use the correct actor's name (do not overwrite it with dialogue text)
            string actorName = DialogueTitleText.text;  // Preserve the actor's name from the title

            // Log for //Debugging
            ////Debug.Log($"SelectResponse - Actor: {actorName}, Next Dialogue Text: {nextResponse.dialogueText}");

            // Update the dialogue using the same actor name, but with the new dialogue text for the next node
            UpdateDialogue(actorName, nextResponse);  // Pass actor name + updated dialogue text
        }
        else
        {
            HideDialogue();
        }
    }//END SelectResponse()


 
    /// <summary>
    /// Hide the dialogue UI
    /// </summary>
    public void HideDialogue()
    {
        //L - Main container to dialogue deactivated. To view initial value see DialogueManager in inspector
        DialogueParent.SetActive(false);


        //L - Re-lock cursor & camera after dialogue disappears
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameObject.FindWithTag("MainCamera").GetComponent<camera>().frozen = false;
    }//END HideDialogue()
 
    /// <summary>
    /// Show the dialogue UI
    /// </summary>
    private void ShowDialogue()
    {
        //L - Main container to dialogue activated. To view initial value see DialogueManager in inspector
        DialogueParent.SetActive(true);

        //L - Fixes weird case where dialogue with response is skipped because previous actor dialogue had no 
        //      responses and failed to update variable when setting parent inactive.
        skippable = false;
    }//END ShowDialogue()
 
    /// <summary>
    /// Check if dialogue is currently active
    /// </summary>
    /// <returns>bool</returns>
    public bool IsDialogueActive()
    {
        return DialogueParent.activeSelf;
    }//END IsDialogueActive

    /// <summary>
    /// L - Search through variable currentDialogue to find node with matching ID
    /// </summary>
    /// <param name="id">Node ID name/number</param>
    /// <returns>DialogueNode</returns>
     private DialogueNode GetDialogueNodeById(string id)
    {
        //if (currentDialogue == null)
        //{
            ////Debug.Log($"currentDialogue is null");
        //}
        // Assuming all nodes are stored in the current dialogue
        return currentDialogue.dialogueNodes.FirstOrDefault(node => node.id == id);
    }//END GetDialogueNodeById()


    /// <summary>
    /// Creates response buttons for dialogue for each response in node
    /// </summary>
    /// <param name="responses">list of responses in node</param>
    private void CreateResponseButtons(List<DialogueResponse> responses)
    {
        // Clear any previous buttons
        foreach (Transform child in responseButtonContainer)
        {
            Destroy(child.gameObject);
        }//END FOREACH

        // Create a button for each response in the current node
        foreach (DialogueResponse response in responses)
        {
            GameObject buttonObj = Instantiate(responseButtonPrefab, responseButtonContainer);

            // Set the response text on the button
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = response.responseText;

            // When the button is clicked, handle the response selection
            buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectResponse(response));
        }//END FOREACH
    }//END CreateResponseButtons()


    /// <summary>
    /// L - Searches for GameObject using tag containing the input argument's actor name. 
    //      Once found, gets its actor from Actor.cs component NpcDialogueActor.
    /// </summary>
    /// <param name="tag">Name of tag (supposed to be named equivalent to actor name)</param>
    public void changeCurrentActor(string tag)
    {
        //L - see function summary
        npcActor = GameObject.FindWithTag(tag).GetComponent<NpcDialogueActor>();
    }//END changeCurrentActor()

    /// <summary>
    /// L - In the event of instance not being created yet, function can be used to store new actor in variable temporarily
    /// </summary>
    /// <param name="otherActor">Any single actor from GameObject using Actor.cs's NpcDialogueActor</param>
    public void changeCurrentDialogue(NpcDialogueActor otherActor)
    {
        //L - see function summary
        currentDialogue = otherActor.Dialogue;
    }//END changeCurrentDialogue()


    /// <summary>
    /// L - Allows the creation of new dialogue when desired node is not within the current actor's dialogue.
    ///     (Searches through other GameObjects in scene via tag)
    /// </summary>
    /// <param name="linkActor">String of actor's name used to search by tag and assign to title</param>
    /// <param name="linkNodeId">The desired ID found from the desired actor's dialogue</param>
    public void SpeakToNewActor(string linkActor, string linkNodeId)
    {
        changeCurrentActor(linkActor);      //L - uses desired actor to find and assign external GameObject NpcDialogueActor from Actor.cs to variable
        changeCurrentDialogue(npcActor);    //L - uses the newly updated variable within DialogueManager to grab new Dialogue appropriate to actor. Then stores in DialogueManager's variable.
        DialogueNode newNode = GetDialogueNodeById(linkNodeId);     //L - uses argument 2's ID to search for dialogue within the just updated currentDialogue
        currentDialogueText = newNode.dialogueText;     //L - uses the newly found dialogue node to pull text from and store in variable
        StartDialogue(npcActor.ActorName, newNode);     //L - Now that all fields have been updated to accomodate for dialogue and actors that were not in an instance,
                                                        //      create UI with appropriately stored variables
    }//END SpeakToNewActor()
}//END class DialogueManager