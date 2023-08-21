using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerClimbState", fileName = "PlayerClimbState")]
public class PlayerClimbState : PlayerBaseState
{
    private Vector3 leftHandPosition;
    private Vector3 rightHandPosition;
    private Vector3 rightFootPosition;
    public override void Enter()
    {
        base.Enter();
        
       
        switch (parameter.currentPlayerMovement)
        {
            case PlayerSensor.NextPlayerMovement.climbLow:
                parameter.animator.CrossFadeInFixedTime("Climb",0.1f);
                leftHandPosition = parameter.playerSensor.Ledge +
                                   Vector3.Cross(parameter.playerSensor.ClimbHitNormal, Vector3.up) * 0.3f;
                break;
            case PlayerSensor.NextPlayerMovement.climbHigh:
                parameter.animator.CrossFadeInFixedTime("ClimbHigh",0.1f);
                rightHandPosition = parameter.playerSensor.Ledge +
                                    Vector3.Cross(parameter.playerSensor.ClimbHitNormal, Vector3.up)
                                    * 0.3f;
                rightFootPosition = parameter.playerSensor.Ledge + Vector3.down * 1.2f;
                break;
            case PlayerSensor.NextPlayerMovement.vault:
                parameter.animator.CrossFadeInFixedTime("JumpOver",0.1f);
                rightHandPosition = parameter.playerSensor.Ledge;
                leftHandPosition = parameter.playerSensor.Ledge +
                                   Vector3.Cross(-parameter.playerSensor.ClimbHitNormal, Vector3.up * 0.3f);
                break;
            
        }
        
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        parameter.playerTr.rotation = Quaternion.Lerp(parameter.playerTr.rotation,
            Quaternion.LookRotation(-parameter.playerSensor.ClimbHitNormal),0.5f);
        /*
        if (parameter.currentPlayerMovement == PlayerSensor.NextPlayerMovement.climbLow)
        {
          //  parameter.animator.MatchTarget(leftHandPosition,quaternion.identity, AvatarTarget.LeftHand,new MatchTargetWeightMask(Vector3.one, 
            //    0f),0,0.1f);
        }
        if (parameter.currentPlayerMovement == PlayerSensor.NextPlayerMovement.climbHigh)
        {
            parameter.animator.MatchTarget(rightHandPosition,quaternion.identity, AvatarTarget.RightHand,new MatchTargetWeightMask(Vector3.one, 
                0f),0,0.13f);
            parameter.animator.MatchTarget(rightFootPosition,quaternion.identity, AvatarTarget.RightFoot,new MatchTargetWeightMask(Vector3.one, 
                0f),0.2f,0.32f);
        }
        if (parameter.currentPlayerMovement == PlayerSensor.NextPlayerMovement.vault)
        {
            parameter.animator.MatchTarget(rightHandPosition,quaternion.identity, AvatarTarget.RightHand,new MatchTargetWeightMask(Vector3.one, 
                0f),0,0.2f);
            parameter.animator.MatchTarget(rightFootPosition+Vector3.up*0.1f,
                quaternion.identity, AvatarTarget.RightFoot,new MatchTargetWeightMask(Vector3.one, 
                0f),0.35f,0.45f);
        }
        */
        AnimatorStateInfo animatorStateInfo = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.normalizedTime > 0.95f&& animatorStateInfo.IsTag("climb"))
        {
            stateMachine.ChangeState(stateMachine.states[typeof(PlayerFreeLookState)]);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        
    }
}
