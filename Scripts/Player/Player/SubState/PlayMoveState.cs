using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Move", fileName = "PlayerState_Move")]
public class PlayMoveState : PlayerGroundedState
{
   
    public override void Enter()
    {
        base.Enter();

        Debug.Log("Enter Move");
        Debug.Log(movement.name);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(input.axesX==0)
        {
            stateMachine.SwitchState(stateMachine.stateTable[typeof(PlayerIdleState)]);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        movement.SetVelocityX(input.axesX * playerData.walkSpeed);
    }
}
