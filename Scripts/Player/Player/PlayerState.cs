using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject,IState
{
    
    protected PlayerStateMachine stateMachine;
    protected PlayerInput input;
    protected Core core;
    protected PlayerData playerData;

    public void Initialize(PlayerStateMachine stateMachine,PlayerInput playerInput,Core core,PlayerData playerData)
    {
        this.stateMachine = stateMachine;
        this.input = playerInput;
        this.core = core;
        this.playerData = playerData;
    }
    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void LogicUpdate()
    {
       
    }

    public virtual void PhysicUpdate()
    {
        
    }

  
}
