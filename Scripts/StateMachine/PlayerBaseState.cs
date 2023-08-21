using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : ScriptableObject, State
{
    protected PlayerStateMachine stateMachine;

    protected Parameter parameter;

    protected InputReader inputReader;

    public void Initialize(PlayerStateMachine stateMachine, Parameter parameter, InputReader inputReader)
    {
        this.stateMachine = stateMachine;
        this.parameter = parameter;
        this.inputReader = inputReader;
    }
    public Vector3 SetDirection()
    {
        Vector3 forward = parameter.mainCameraTr.forward;
        Vector3 right = parameter.mainCameraTr.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * inputReader.moveValue.y + right * inputReader.moveValue.x;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        parameter.characterController.Move(motion * deltaTime);

        //Debug.Log(parameter.forceReceiver.Movement);
    }

    protected void Gravity(float deltaTime)
    {
        parameter.characterController.Move(parameter.forceReceiver.Movement * deltaTime);
    }
    protected void FaceTarget()
    {
        if (parameter.targeter.currentTarget == null) return;
        Vector3 lookPos = parameter.targeter.currentTarget.trans.position - parameter.playerTr.position;
        lookPos.y = 0;
        Quaternion a = Quaternion.LookRotation(lookPos);

        parameter.playerTr.rotation = Quaternion.Lerp(parameter.playerTr.rotation, a, parameter.playerData.turnSpeed * Time.deltaTime);


    }

    protected void PlayerAttack()
    {
        //Debug.Log("attack");
        stateMachine.ChangeState(stateMachine.states[typeof(PlayerAttackState)]);

    }
    protected void Roll()
    {
        stateMachine.ChangeState(stateMachine.states[typeof(PlayerRollState)]);
    }

   



    public virtual void Enter()
    {

    }

    public virtual void Tick(float deltaTime)
    {

    }

    public virtual void Exit()
    {

    }
}
