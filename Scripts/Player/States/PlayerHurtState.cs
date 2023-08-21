using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/ PlayerHurtState", fileName = " PlayerHurtState")]
public class PlayerHurtState : PlayerBaseState
{
    private float force;
    public override void Enter()
    {
        base.Enter();
        parameter.animator.CrossFadeInFixedTime("2Hand-Sword-GetHit-B1", 0.1f);
        force = parameter.force;
        Debug.Log("Been hurting");
       
        //Debug.Log("force"+force);
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        
        if (force != 0)
        {
            parameter.characterController.enabled = false;
            parameter.playerTr.position= Vector3.Lerp( parameter.playerTr.position, parameter.pushedTr.position, force * Time.deltaTime);
            parameter.characterController.enabled = true;
            //Debug.Log(parameter.playerTr.position);
            //float distance = Vector3.Distance(enemyStateMachine.transform.position, enemyStateMachine.test_transform.position);

        }
        

        AnimatorStateInfo currentStateInfo = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (currentStateInfo.normalizedTime > 0.95f&&currentStateInfo.IsTag("hurt"))
        {
            if(parameter.is_targeting)
            stateMachine.ChangeState(stateMachine.states[typeof(PlayerTargetingState)]);
            else
            {
                stateMachine.ChangeState(stateMachine.states[typeof(PlayerFreeLookState)]);
                
            }
        }
        
            
        
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
