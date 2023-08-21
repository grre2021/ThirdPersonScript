using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[DisallowMultipleComponent]
public class MeunController :Singleton<MeunController>
{
    [Header("Meun")]
    [SerializeField]
    private Page InitialPage;
    [SerializeField]
    private GameObject firstFocusItem;

    [SerializeField] private Page MeunPage;

    [SerializeField] 
    private bool isMeun;

    [Header("Quest")] 
    [SerializeField] private GameObject QuestPanel;

    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject finished;

    [Header("Slider")]
    [SerializeField] private Slider sliderPlayer;
    [SerializeField] private Slider sliderEnemy;

    [Header("LoadUI")] [SerializeField] private GameObject LoadPanel;
    [SerializeField] private Slider loadSlider;
    [SerializeField] private TextMeshProUGUI TextMeshProUGUI;

    [Header("Sound")] [SerializeField] private AudioClip clickSound;
    

    private Canvas rootCanvas;

    private Stack<Page> pagesStack = new Stack<Page>();

    protected override void Awake()
    {
        base.Awake();
        rootCanvas = GetComponent<Canvas>();
       // DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        if(!isMeun)
        EventCenter.Instance.AddEventListener(eventInput.Menu,MeunOpen );
    }

    private void OnDisable()
    {
        if(!isMeun)
        EventCenter.Instance.RemoveEventListener(eventInput.Menu,MeunOpen );
    }


    private void Start()
    {
        if (firstFocusItem != null)
        {
            EventSystem.current.SetSelectedGameObject(firstFocusItem);
        }
        if (InitialPage&&isMeun)
        {
            PushPage(InitialPage);

        }
        if(isMeun) return;
        start.SetActive(false);
        finished.SetActive(false);
        QuestPanel.SetActive(false);
        sliderEnemy.gameObject.SetActive(false);
        
    }

    /// <summary>
    /// 调用此函数完成页面切换
    /// </summary>
    /// <param name="page"></param>
    public void PushPage(Page page)
    {
        if(!page.gameObject.activeSelf)
            page.gameObject.SetActive(true);
        
        page.Entry(false);
        
        SoundManager.Instance.PlayOneShot(clickSound);

        if (pagesStack.Count > 0)
        {
            Page currentPage = pagesStack.Peek();
            if (currentPage.exitOnNewPagePush)
            {
                currentPage.Exit(false);
            }
        }
        pagesStack.Push(page);

    }

    public void PopPage()
    {
        if (pagesStack.Count > 1)
        {
            Page page = pagesStack.Pop();
            page.Exit(false);
            SoundManager.Instance.PlayOneShot(clickSound);

            Page newCurrentPage = pagesStack.Peek();
            if (newCurrentPage.exitOnNewPagePush)
            {
                newCurrentPage.Entry(false);
            }
        }
        else
        {
            Debug.LogWarning("Tring to pop a page but only 1 page remains in the stack!");

        }

    }

    public void StartQuest(string questId)
    {
        questText.text = questId;
        QuestPanel.SetActive(true);
        start.SetActive(true);
    }

    public void UpdateQuest()
    {
        if (QuestPanel.activeSelf)
        {
            start.SetActive(false);
            finished.SetActive(true);
        }
    }

    public void FinishQuest()
    {
        start.SetActive(false);
        finished.SetActive(false);
        QuestPanel.SetActive(false);
    }

    public void SycPlayerBlood(float total,float current)
    {
        sliderPlayer.maxValue = total;
        
        
        sliderPlayer.value = current;
    }
    
    public void SycEnemyBlood(float total,float current)
    {
        sliderEnemy.maxValue = total;
        
        sliderEnemy.value = current;
    }

    public void IsActiveEnemyBlood(bool isActive)
    {
        sliderEnemy.gameObject.SetActive(isActive);
    }

    public void MeunOpen()
    {
        if (MeunPage.gameObject.activeSelf)
        {
            MeunPage.gameObject.SetActive(false);
            
        }
        else
        {
          
            PushPage(MeunPage);
        }
    }

    public void Load(float value)
    {
        LoadPanel.SetActive(true);
        loadSlider.value = value;
        TextMeshProUGUI.text = value * 100 + "%";
    }



}