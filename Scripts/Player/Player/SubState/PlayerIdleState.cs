using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Idle",fileName ="PlayerState_Idle")]
public class PlayerIdleState : PlayerGroundedState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Idle");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        if(input.axesX!=0)
        {
            stateMachine.SwitchState(stateMachine.stateTable[typeof(PlayMoveState)]);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
    

