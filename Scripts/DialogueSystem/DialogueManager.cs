using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
public class DialogueManager : Singleton<DialogueManager>
{
      [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    //��ʱ�����UI Took it �޸�
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choice UI")]
    [SerializeField] private GameObject[] choiceUI;

    private TextMeshProUGUI[] choicesText;

    private TextMeshProUGUI choiceText;

    private Story currentStory;


    public bool dialogueIsPlaying;

    private void Start()
    {
        dialoguePanel.SetActive(false);

        dialogueIsPlaying = false;
        //get all of the choices text
        choicesText = new TextMeshProUGUI[choiceUI.Length];
        int index = 0;
        foreach (GameObject choice in choiceUI)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    public void EnterNewDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            //set texy for the current dialogue line
            dialogueText.text = currentStory.Continue();
            //display choices ,if any,for this dialogue line
            DisplayChoice();
        }
        else
        {
            ExitDialogue();
        }
    }
    private void ExitDialogue()
    {
        dialoguePanel.SetActive(false);
        for (int i = 0; i < choiceUI.Length; i++)
        {
            choiceUI[i].SetActive(false);
        }
        
        dialogueText.text = "";
        dialogueIsPlaying = false;
        EventCenter.Instance.EventTrigger(eventDialogue.FINISHED);
    }

    private void DisplayChoice()
    {
        List<Choice> currentChoice = currentStory.currentChoices;
        Debug.Log("current choice"+currentChoice.Count);
        if (currentChoice.Count > choiceUI.Length)
        {
            Debug.LogError("More choices were given than the UI support. Number of choices given :" + currentChoice.Count);
        }

        int index = 0;
        //enable and initial the choices up to the amout of choices for this line of dialogue
        foreach (Choice choice in currentChoice)
        {
            
            choiceUI[index].SetActive(true);
            choicesText[index].text = choice.text;
            index++;

        }
        //go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choiceUI.Length; i++)
        {
            choiceUI[index].SetActive(false);
        }

        StartCoroutine(nameof(SelectFristChoice));
    }

    private IEnumerator SelectFristChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choiceUI[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        for (int i = 0; i < choiceUI.Length; i++)
        {
            choiceUI[i].SetActive(false);
        }
        ContinueStory();
    }
}
