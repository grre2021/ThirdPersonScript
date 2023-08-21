using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerTargetingState", fileName = "PlayerTargetingState")]
public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");

    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");

    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");




    public override void Enter()
    {
        inputReader.CancelEvent += OnCancel;
        inputReader.Attack += PlayerAttack;
        inputReader.Roll += Roll;
        if (!parameter.is_targeting)
        {
            if (!parameter.targeter.SelectTarget())
            {
                OnCancel();
                return;
            }
        }
        parameter.animator.CrossFadeInFixedTime(TargetingBlendTreeHash, 0.1f);
        parameter.is_targeting = true;
        Debug.Log("enter targting");
    }

    public override void Exit()
    {
        Debug.Log("exit targting");
        inputReader.CancelEvent -= OnCancel;
        inputReader.Attack -= PlayerAttack;
        inputReader.Roll -= Roll;

    }

    public override void Tick(float deltaTime)
    {
        if (parameter.targeter.currentTarget == null)
        {
            stateMachine.ChangeState(stateMachine.states[typeof(PlayerFreeLookState)]);
            Debug.Log("test");
            parameter.is_targeting = false;
            return;
        }
        //Move(CalculateMovement() * parameter.playerData.TargetingSpeed, deltaTime);
        UpdateAnimator(deltaTime);
        FaceTarget();

    }
    void OnCancel()
    {
        parameter.is_targeting = false;
        stateMachine.ChangeState(stateMachine.states[typeof(PlayerFreeLookState)]);

    }

    Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += parameter.playerTr.forward * inputReader.moveValue.y;
        movement += parameter.playerTr.right * inputReader.moveValue.x;

        return movement;
    }

    void UpdateAnimator(float deltaTime)
    {
        if (inputReader.moveValue.x == 0)
        {
            parameter.animator.SetFloat(TargetingRightHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = inputReader.moveValue.x > 0 ? 1f : -1f;
            //Debug.Log(value);
            parameter.animator.SetFloat(TargetingRightHash, value, 0.1f, deltaTime);
        }

        if (inputReader.moveValue.y == 0)
        {
            parameter.animator.SetFloat(TargetingForwardHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = inputReader.moveValue.y > 0 ? 1f : -1f;
            parameter.animator.SetFloat(TargetingForwardHash, value, 0.1f, deltaTime);
        }
    }


}
