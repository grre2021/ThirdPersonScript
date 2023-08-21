using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected Movement movement { get=> core.GetCoreComponent<Movement>(); }

    protected bool isAbilityDone;

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isAbilityDone)
        {
            Debug.Log("Ability is done");
            stateMachine.SwitchState(stateMachine.stateTable[typeof(PlayerIdleState)]);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    
}
