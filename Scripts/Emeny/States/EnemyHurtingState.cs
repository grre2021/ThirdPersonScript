using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/EnemyHurtingState", fileName = " EnemyState/EnemyHurtingState")]
public class EnemyHurtingState : EnemyBaseState
{
    
    float force;
    public override void Enter()
    {
        base.Enter();
        
        
        enemyStateMachine.animator.Play("Hurt", 0, 0f);
         
        force = enemyStateMachine.force;
        //if (force != 0)
        // enemyStateMachine.enemyRigidbody.AddForce(enemyStateMachine.hurtDirection * 3, ForceMode.Impulse);



        //rigidbody.AddForce()
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        if (force != 0)
        {
            enemyStateMachine.transform.position = Vector3.Lerp(enemyStateMachine.transform.position, enemyStateMachine.test_transform.position, force * Time.deltaTime);
            //float distance = Vector3.Distance(enemyStateMachine.transform.position, enemyStateMachine.test_transform.position);
            //Debug.Log(enemyStateMachine.transform.position);

        }
        if (isFinishAnim())
            enemyStateMachine.ChangeState(enemyStateMachine.states[typeof(EnemyChashingStatr)]);


    }
    public override void Exit()
    {
        base.Exit();

    }




}
