using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerFreeLookState", fileName = "PlayerFreeLookState")]
public class PlayerFreeLookState : PlayerBaseState
{

    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("Free Look Blend Tree");
    public override void Enter()
    {
        inputReader.TargetEvent += OnTarget;
        inputReader.Attack += PlayerAttack;
        inputReader.Roll += Roll;
        inputReader.ClimbEvent += stateMachine.Climbing;

        parameter.targeter.Cancel();
        //parameter.animator.Play(FreeLookBlendTreeHash);
        parameter.animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, 0.1f);
        // Debug.Log("freelookstate");
    }

    public override void Exit()
    {
        inputReader.TargetEvent -= OnTarget;
        inputReader.Attack -= PlayerAttack;
        inputReader.Roll -= Roll;
        inputReader.ClimbEvent -= stateMachine.Climbing;
    }

    public override void Tick(float deltaTime)
    {
        Gravity(deltaTime);
        if (inputReader.moveValue == Vector2.zero)
        {
            parameter.animator.SetFloat("FreeLookSpeed", 0, 0.1f, deltaTime);
        }
        else
        {

            parameter.animator.SetFloat("FreeLookSpeed", 1, 0.1f, deltaTime);
            //Debug.Log(parameter.animator.GetFloat("FreeLookSpeed"));
            

            Vector3 movement = SetDirection();

            parameter.characterController.Move(movement * parameter.playerData.freeLookWalkSpeed * deltaTime);
            parameter.playerTr.rotation =

            Quaternion.Lerp(parameter.playerTr.rotation, Quaternion.LookRotation(movement), parameter.playerData.freeLookRotationDamping * deltaTime);

        }


    }



    void OnTarget()
    {
        stateMachine.ChangeState(stateMachine.states[typeof(PlayerTargetingState)]);
    }


}
