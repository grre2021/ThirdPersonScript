using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : Singleton<EventCenter>
{
    Dictionary<Enum, IEventInfo> eventDic = new Dictionary<Enum, IEventInfo>(); 
    
    /// <summary>
    /// 添加监听事件,并写个重载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="event_name"></param>
    /// <param name="action"></param>
    public  void AddEventListener<T>(Enum typeEvent,UnityAction<T> action)
    {
        if(eventDic.ContainsKey(typeEvent))
        {
            (eventDic[typeEvent] as EventInfo<T>).actions += action;
        }
        else
        {
            eventDic.Add(typeEvent, new EventInfo<T>(action));
        }
    }

    public void AddEventListener(Enum typeEvent, UnityAction action)
    {
        if (eventDic.ContainsKey(typeEvent))
        {
            (eventDic[typeEvent] as EventInfo).actions += action;
        }
        else
        {
            eventDic.Add(typeEvent, new EventInfo(action));
        }
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="typeEvent"></param>
    /// <param name="info"></param>

    public void EventTrigger<T>(Enum typeEvent, T info)
    {
        if (eventDic.ContainsKey(typeEvent))
        {
            (eventDic[typeEvent] as EventInfo<T>).actions?.Invoke(info);
        }
    }
    public void EventTrigger(Enum typeEvent)
    {
        if (eventDic.ContainsKey(typeEvent))
        {
            (eventDic[typeEvent] as EventInfo).actions?.Invoke();
        }
    }


    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="typeEvent"></param>
    /// <param name="action"></param>
    public void RemoveEventListener<T>(Enum typeEvent, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(typeEvent))
        {
            (eventDic[typeEvent] as EventInfo<T>).actions -= action;
        }
   
    }

    public void RemoveEventListener(Enum typeEvent, UnityAction action)
    {
        if (eventDic.ContainsKey(typeEvent))
        {
            (eventDic[typeEvent] as EventInfo).actions -= action;
        }
   
    }

    /// <summary>
    /// 清楚所以事件
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}
