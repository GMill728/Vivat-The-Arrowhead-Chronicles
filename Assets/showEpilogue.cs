using UnityEngine;

public class showEpilogue : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialogueManager.Instance.linkActorVar = "EpilDialObj";  //update DialogueManager's temp variables for circumstances requiring these strings
        DialogueManager.Instance.linkNodeIdVar = "1";
        DialogueManager.Instance.SpeakToNewActor("EpilDialObj", "1");
    }
}
