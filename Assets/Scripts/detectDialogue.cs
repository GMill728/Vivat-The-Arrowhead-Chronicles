//Created by Luke to trigger dialogue when player enters object's FOV
//Created: 4/27/25

using UnityEngine;

public class detectDialogue : MonoBehaviour
{
    private bool detected = false; //don't repeat the popup dialogue or player is stuck
    

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<FieldOfView>().playerinFOV == true && detected == false) //Check if player is in FOV
        {
            detected = true; //Don't repeat this update
            GetComponent<NpcDialogueActor>().Interact(); //Call dialogue through interact script
        }
    }
}
