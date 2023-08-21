using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    private Dictionary<string, Quest> questMap;

    //quest start requirements
    private int currentPlayerLevel=1;
    
    private void Awake()
    {
        questMap = CreatQuestMap();
        
        foreach (Quest quest in questMap.Values)
        {
            Debug.Log(quest.info.id);
        }

        /*
                //Testing
                Quest quest = GetQuestById("CollectCoinsQuest");
                Debug.Log(quest.info.displayName);
                Debug.Log(quest.info.levelRequirement);
                Debug.Log(quest.states);
                */

    }

    private void OnEnable()
    {
        //添加startQuest...的事件
        EventCenter.Instance.AddEventListener<string>(eventQuest.onStartQuest, StartQuest);
        EventCenter.Instance.AddEventListener<string>(eventQuest.onAdvanceQuest, AdvanceQuest);
        EventCenter.Instance.AddEventListener<string>(eventQuest.onFinishedQuest, FinishedQuest);
        

    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener<string>(eventQuest.onStartQuest, StartQuest);
        EventCenter.Instance.RemoveEventListener<string>(eventQuest.onAdvanceQuest, AdvanceQuest);
        EventCenter.Instance.RemoveEventListener<string>(eventQuest.onFinishedQuest, FinishedQuest);

    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.states = state;
        EventCenter.Instance.EventTrigger<Quest>(eventQuest.onQuestStateChange,quest);
        //调用questevents.questatechange,并传入quest
    }

    private bool CheckRequireMentsMet(Quest quest)
    {
        bool meetRequirements = true;

        //check player level requirement
        if (currentPlayerLevel < quest.info.levelRequirement)
        {
            meetRequirements = false;
            Debug.Log(quest.info.id+"need to raise player level ");
        }

        //check quest prerequisites for completion
        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            if (GetQuestById(prerequisiteQuestInfo.id).states != QuestState.FINISHED)
            {
                Debug.Log(quest.info.id+" prerequisiteQuestInfo isn't finished");
                meetRequirements = false;
            }
        }

        return meetRequirements;
    }

    private void Update()
    {
        //这个地方应该还有优化的空间
        //loop through ALL quests
        foreach (Quest quest in questMap.Values)
        {
            //if we're now meting the requirements ,switch over to the CAN_START state

            if (quest.states == QuestState.REQUIREMENTS_NOT_MET && CheckRequireMentsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
                //Debug.Log(quest.info.id+quest.states);

            }

        }
    }

    private void StartQuest(string id)
    {
        Debug.Log("StartQuest " + id);
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id,QuestState.IN_PROGRESS);
        MeunController.Instance.StartQuest(id);

    }

    private void AdvanceQuest(string id)
    {
        Debug.Log("AdvanceQuest " + id);
        Quest quest = GetQuestById(id);
        
        //move on to the next step
        quest.MoveToNextStep();
        //if there are more steps,instantiate the next one
        if (quest.CurrentSteoExists())
        {
            quest.InstantiateCurrentCurrentQuestStep(this.transform);
        }
        //if there are no more step,then we'have finished all of them
        else
        {
            ChangeQuestState(quest.info.id,QuestState.CAN_FINISH);
            MeunController.Instance.UpdateQuest();
        }
        

    }
    private void FinishedQuest(string id)
    {
        Debug.Log("FinishedQuest " + id);
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id,QuestState.FINISHED);
        MeunController.Instance.FinishQuest();

    }
/// <summary>
/// questRewards
/// 任务奖励
/// </summary>
/// <param name="quest"></param>
    private void ClaimRewards(Quest quest)
    {
        
    }

    private void Start()
    {
        //broadcost the initial state of all quests on startup
        foreach (Quest quest in questMap.Values)
        {
            //添加QuestStateChange事件
            EventCenter.Instance.EventTrigger<Quest>(eventQuest.onQuestStateChange,quest);
        }
    }

    private Dictionary<string, Quest> CreatQuestMap()
    {
        //loads all QuestInfoSO Scriptable Objects under the Assets/Resources/Quests folder

        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        //Create the quest map
        Dictionary<string, Quest> idToQurstMap = new Dictionary<string, Quest>();

        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQurstMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
            }
            idToQurstMap.Add(questInfo.id, new Quest(questInfo));
        }
        return idToQurstMap;

    }

    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogWarning("ID not found in the Quest Map: " + id);
        }
        return quest;
    }




}

