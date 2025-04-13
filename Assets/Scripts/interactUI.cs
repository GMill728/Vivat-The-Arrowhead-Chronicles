/*
Script handles displaying UI text when player is near interactable objects.
Written by: Luke
On: 4/12/25
Last Modified: 4/12/25
*/
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private Interactor playerInteractor;
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private DialogueManager DialogueController;

    private void Start()
    {
        Hide(); //No interactUI appearing when game loads initially.
    }

    private void Update()
    {
        if (playerInteractor.GetInteractInfo() != null && !DialogueController.IsDialogueActive()) //If player detects interactable object and no dialogue is on the screen
        {
            textUI.text = "F) " + playerInteractor.GetInteractInfo();
            Show(); //reveal UI notification
        }
        else
        {
            Hide(); //conceal UI notification
        }
    }


    private void Show()
    {
        containerGameObject.SetActive(true); //enable parent containing UI
    }

    private void Hide()
    {
        containerGameObject.SetActive(false); //disable parent containing UI
    }
}
