using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLcon : MonoBehaviour
{
    [SerializeField] private GameObject requirementsNotMetToStartIcon;

    [SerializeField] private GameObject canStartIcon;

    [SerializeField] private GameObject requirementsNotMetToFinishIcon;

    [SerializeField] private GameObject canFinishIcon;

    

    public void SetState(QuestState newState, bool startPoint, bool finishPoint)
    {
        requirementsNotMetToStartIcon.SetActive(false);
        canStartIcon.SetActive(false);
        requirementsNotMetToFinishIcon.SetActive(false);
        canFinishIcon.SetActive(false);

        //set the oppropriate one to active based on the new state
        switch (newState)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                requirementsNotMetToStartIcon.SetActive(true);
                break;
            case QuestState.CAN_START:
                canStartIcon.SetActive(true);
                break;
            case QuestState.IN_PROGRESS:
                requirementsNotMetToFinishIcon.SetActive(true);
                break;
            case QuestState.CAN_FINISH:
                canFinishIcon.SetActive(true);
                break;
            case QuestState.FINISHED:
                canFinishIcon.SetActive(false);
                break;
            default:
                Debug.LogWarning("Quest State not recognized by switch statement for quest icon: " + newState);
                break;
        }
    }
}
