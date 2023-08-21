using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/EnemyChashingState", fileName = "EnemyChashingState")]
public class EnemyChashingStatr : EnemyBaseState
{
    Transform target;
    public override void Enter()
    {
        base.Enter();
        MeunController.Instance.IsActiveEnemyBlood(true);
        enemyStateMachine.animator.CrossFadeInFixedTime("Locomotion", 0.1f);

        enemyStateMachine.rootMotion.chashingTrigger();
        target = enemyStateMachine.player;
        enemyStateMachine.navMeshAgent.stoppingDistance = 3f;

    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);

        enemyStateMachine.navMeshAgent.SetDestination(target.position);
        enemyStateMachine.animator.SetFloat("walkspeed", enemyStateMachine.navMeshAgent.velocity.magnitude);

        if (Vector3.Distance(enemyStateMachine.navMeshAgent.destination, enemyStateMachine.navMeshAgent.nextPosition) <=
        enemyStateMachine.navMeshAgent.stoppingDistance)
        {

            // Debug.Log("chasing stopped");
            // Debug.Log("distance to player  " + XZDistance(enemyStateMachine.transform, enemyStateMachine.player));
            enemyStateMachine.animator.SetFloat("walkspeed", 0f);
            enemyStateMachine.ChangeState(enemyStateMachine.states[typeof(EnemyAttackCenterState)]);
        }

    }

    public override void Exit()
    {
        base.Exit();
        //        Debug.Log("chasing exit");
        enemyStateMachine.rootMotion.chashingTrigger();
    }

}
