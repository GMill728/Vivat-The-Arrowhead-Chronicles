/*
Script handles recognizing other objects containing scripts associated with itself in order to
call the IInteractable script Interact() that stems from here.
Written by: Luke
On: 4/6/25
Last Modified: 4/6/25
*/

// Created by Luke using source code from Youtube 
// channel Rytech at https://www.youtube.com/watch?v=K06lVKiY-sY

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactor : MonoBehaviour
{
    //Object where the raycast position will begin
    public Transform InteractorSource;
    //distance the raycast object will extend to search
    public float InteractRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // player presses F
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward); //Ray beginning from designated source to designated distance
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))  //Makes contact
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) //Retrieves IInteractable object's Interact()
                {
                    interactObj.Interact(); //Calls the object's function
                }//END IF
            }//END IF
        }//END IF
    }//END Update()
}//END class Interactor

//Enables other "IInteractable" scripts to trigger actions within an Interact() function that
//  this interactor will interact with.
interface IInteractable
{
    public void Interact();
}
