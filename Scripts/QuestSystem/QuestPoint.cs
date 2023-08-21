using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint;

    [Header("Config")]
    [SerializeField] private bool startPoint;
    [SerializeField] private bool finishPoint;

    private bool playerIsNear = false;

    private string questId;
    private QuestState currentQuestSate;

    private QuestLcon questLcon;

    private void Awake()
    {
        questId = questInfoForPoint.id;
        questLcon = GetComponentInChildren<QuestLcon>();

    }
    private void OnEnable()
    {
        //为事件QuestStateChange添加QuestStateChange函数
        EventCenter.Instance.AddEventListener<Quest>(eventQuest.onQuestStateChange,QuestStateChange);
        EventCenter.Instance.AddEventListener(eventDialogue.FINISHED,SubmitPressed);
    }

    void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener<Quest>(eventQuest.onQuestStateChange,QuestStateChange);
        EventCenter.Instance.RemoveEventListener(eventDialogue.FINISHED,SubmitPressed);

    }
    private void SubmitPressed()
    {
        //Debug.Log("Invoke Quest");
        if (!playerIsNear)
        {
            return;
        }
        Debug.Log("currentQuestState: "+currentQuestSate);

        //启动questevent的startquest，advance，finished事件，并传入quest ID
        if (currentQuestSate.Equals(QuestState.CAN_START) && startPoint)
        {
            //启动StartQuest事件
            EventCenter.Instance.EventTrigger<string>(eventQuest.onStartQuest,questId);
            Debug.Log("Start Quest");
            
        }
        else if (currentQuestSate.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            //启动FinishQuest事件
            EventCenter.Instance.EventTrigger<string>(eventQuest.onFinishedQuest,questId);
        }
    }

    private void QuestStateChange(Quest quest)
    {
        //only update the quest state if this point has the corresponding quest
        if (quest.info.id.Equals(questId))
        {
            currentQuestSate = quest.states;
            if(questLcon!=null)
            questLcon.SetState(currentQuestSate, startPoint, finishPoint);
            Debug.Log("Quest with id: " + questId + " updatede to state: " + currentQuestSate);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
        }

    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
        }

    }

}
