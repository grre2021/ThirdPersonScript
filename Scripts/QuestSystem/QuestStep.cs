using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    private string questId;

    public void InitializedQuestStep(string questId)
    {
        this.questId = questId;
    }

    protected void FinishedQurstStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            //Tood - Advance the quest forward now that we've finished this step
            //启动(invoke)questevents.AdvanceQuest事件
            EventCenter.Instance.EventTrigger<string>(eventQuest.onAdvanceQuest,questId);

            Destroy(this.gameObject);
        }
    }
}
