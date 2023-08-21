using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerStateMachine : StateMachine
{
    //��������״̬
    [SerializeField] PlayerState[] states;
    //�������
    [SerializeField] PlayerInput playerInput;
    //�����ȡ��Ҹ������ܽű�
    [SerializeField]Core core;
    //�������
    [SerializeField] PlayerData playerData;

    

    private void Awake()
    {
        
        //ʵ�����ֵ䣬����Ӹ���״̬
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
