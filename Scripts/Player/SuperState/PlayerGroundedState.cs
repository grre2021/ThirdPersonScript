using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    //ͨ��core��ȡmovement���
    protected Movement movement { get => core.GetCoreComponent<Movement>(); }
    protected Detection detection { get => core.GetCoreComponent<Detection>(); }

    private PlayerJumpState jumpState;
    public override void Enter()
    {
        base.Enter();


    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log(detection.isGround());
        if (!detection.isGround())
        {
            stateMachine.SwitchState(stateMachine.stateTable[typeof(PlayerInAirState)]);
        }


        if (input.jump)
        {
            stateMachine.SwitchState(stateMachine.stateTable[typeof(PlayerJumpState)]);
        }

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
