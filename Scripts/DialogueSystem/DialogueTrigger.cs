using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset inkJson;
    


    //负责启动对话
    public void playDialogue()
    {
        Debug.Log("playDialogue");
        if (DialogueManager.Instance.dialogueIsPlaying)
        {
            DialogueManager.Instance.ContinueStory();
        }
        else
            DialogueManager.Instance.EnterNewDialogue(inkJson);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("playtriggerenter"+other.name);
            EventCenter.Instance.AddEventListener(eventInput.INTERACTION,playDialogue);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventCenter.Instance.RemoveEventListener(eventInput.INTERACTION,playDialogue);
        }
    }
}
