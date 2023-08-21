using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerRollState", fileName = "PlayerRollState")]
public class PlayerRollState : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        if (parameter.is_targeting)
        {
            if (inputReader.moveValue.x != 0)
            {
                if (inputReader.moveValue.x > 0)
                {
                    parameter.animator.CrossFadeInFixedTime("StandingDodgeRight", 0.1f);
                }
                else
                {
                    parameter.animator.CrossFadeInFixedTime("Standing Dodge Left", 0.1f);
                }

            }
            else
            {
                parameter.animator.CrossFadeInFixedTime("DiveRoll", 0.1f);

            }

        }
        else
        {
            parameter.animator.CrossFadeInFixedTime("DiveRoll", 0.1f);
        }


    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        FaceTarget();
        Vector3 movement = SetDirection();

        parameter.characterController.Move(movement * parameter.playerData.dodgeSpeed * deltaTime);
            
        AnimatorStateInfo animatorStateInfo = parameter.animator.GetCurrentAnimatorStateInfo(0);
       // Debug.Log(animatorStateInfo.normalizedTime);

        if (animatorStateInfo.normalizedTime >= 0.9f && animatorStateInfo.IsTag("roll")) 
        {
            Debug.Log(parameter.is_targeting);
            if (parameter.is_targeting)
            {
                stateMachine.ChangeState(stateMachine.states[typeof(PlayerTargetingState)]);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.states[typeof(PlayerFreeLookState)]);
            }
        }
    }

}
