using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Air", fileName = "PlayerState_In_Air")]
public class PlayerInAirState : PlayerState
{
    protected Movement movement { get => core.GetCoreComponent<Movement>(); }
    protected Detection detection { get => core.GetCoreComponent<Detection>(); }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("enter air");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (detection.isGround())
        {
            stateMachine.SwitchState(stateMachine.stateTable[typeof(PlayerIdleState)]);

        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
