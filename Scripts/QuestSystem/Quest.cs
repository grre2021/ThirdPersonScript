using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    //staic info
    public QuestInfoSO info;
    //state info
    public QuestState states;

    private int currentQuestSteoIndex;
    

    public Quest(QuestInfoSO questInfoSO)
    {
        this.info = questInfoSO;
        this.states = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestSteoIndex = 0;
    }
    public void MoveToNextStep()
    {
        currentQuestSteoIndex++;
    }
    public bool CurrentSteoExists()
    {
        return (currentQuestSteoIndex < info.questStepPrefads.Length);

    }

    public void InstantiateCurrentCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefad = GetCurrentQuestPrefad();
        if (questStepPrefad != null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefad, parentTransform)
                .GetComponent<QuestStep>();
            questStep.InitializedQuestStep((info.id));
        }

    }


    private GameObject GetCurrentQuestPrefad()
    {
        GameObject questStepPrefad = null;
        if (CurrentSteoExists())
        {
            questStepPrefad = info.questStepPrefads[currentQuestSteoIndex];
        }
        else
        {
            Debug.LogWarning("Tried to get quest step prefad, but strpIndex was out of range indicating that"
                             + "there's no current step: QuestId=" + info.id + " , stepIndex " + currentQuestSteoIndex);
        }
        return questStepPrefad;
    }
}

public enum QuestState
{
    REQUIREMENTS_NOT_MET,
    CAN_START,
    IN_PROGRESS,
    CAN_FINISH,
    FINISHED
}
