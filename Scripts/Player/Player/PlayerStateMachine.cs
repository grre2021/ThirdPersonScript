using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerStateMachine : StateMachine
{
    //代表所有状态
    [SerializeField] PlayerState[] states;
    //玩家输入
    [SerializeField] PlayerInput playerInput;
    //负责获取玩家各个功能脚本
    [SerializeField]Core core;
    //玩家数据
    [SerializeField] PlayerData playerData;

    

    private void Awake()
    {
        
        //实例化字典，并添加各个状态
        stateTable = new Dictionary<System.Type, IState>(states.Length);
        foreach(PlayerState state in states)
        {
            state.Initialize(this,playerInput,core,playerData);
            stateTable.Add(state.GetType(), state);
        }
       
        
       
    }

    private void Start()
    {

        SwitchOn(stateTable[typeof(PlayerIdleState)]);

        playerInput.EnableGameplayInput();
    }

    

}
