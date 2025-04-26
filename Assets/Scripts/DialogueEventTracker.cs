using UnityEngine;

public class DialogueEventTracker : MonoBehaviour
{
    public DialogueManager dialogueController;
    private bool R5 = false;
    private bool R6 = false;

    public static bool optionB = false;


    private void Update()
    {
        if(dialogueController != null)
        {
            if (dialogueController.responseActorVar == "RiserNine" && dialogueController.activeId == "14" && R5 == false)
            {
                R5 = true; //stops from repeatedly committing action
                R5unlocked();
            }

            if (dialogueController.responseActorVar == "RiserNine" && dialogueController.activeId == "15" && R6 == false)
            {
                R6 = true; //stops from repeatedly committing action
                R6unlocked();
            }
        }
    }

    public void R5unlocked()
    {
        //change starting dialogue
        GameObject.FindWithTag("RiserNine").GetComponent<NpcDialogueActor>().interactDialogueNum = "16";
        Debug.Log("R5unlocked");
    }

    public void R6unlocked()
    {
        //change starting dialogue
        GameObject.FindWithTag("RiserNine").GetComponent<NpcDialogueActor>().interactDialogueNum = "17";
        Debug.Log("R6unlocked");

        optionB = true;
    }
}
