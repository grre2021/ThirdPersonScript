using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/EnemyPartolState", fileName = "EnemyPartolState")]
public class EnemyPartolState : EnemyBaseState
{
    float idleTime;
    List<Vector3> points;
    bool isInPartol;


    public override void Enter()
    {
        base.Enter();
        enemyStateMachine.animator.CrossFadeInFixedTime("Locomotion", 0.1f);
        Debug.Log("Enter Partol State");
        idleTime = RandomIdleTime();
//        Debug.Log(idleTime);
        points = new List<Vector3>(enemyStateMachine.partolPoints.Length);
        foreach (var trPoint in enemyStateMachine.partolPoints)
        {
            points.Add(trPoint.position);

        }
        isInPartol = false;
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        idleTime -= deltaTime;
        Vector3 point = Vector3.zero;
        // Debug.Log(Vector3.Distance(enemyStateMachine.navMeshAgent.destination, enemyStateMachine.navMeshAgent.nextPosition));
        if(!enemyStateMachine.is_standby)
        enemyStateMachine.animator.SetFloat("walkspeed", enemyStateMachine.navMeshAgent.velocity.magnitude);



        if (idleTime < 0 && !isInPartol)
        {
            //Debug.Log("in partol");

            point = points[RandomPoint()];
            //巡逻到此位置
            if(!enemyStateMachine.is_standby)
             enemyStateMachine.navMeshAgent.SetDestination(point);

            isInPartol = true;
        }
        if (isInPartol &&
        Vector3.Distance(enemyStateMachine.navMeshAgent.destination, enemyStateMachine.navMeshAgent.nextPosition) <=
        enemyStateMachine.navMeshAgent.stoppingDistance)
        {
            //Debug.Log("is In Partol");
             enemyStateMachine.animator.SetFloat("walkspeed", 0);
            idleTime = RandomIdleTime();
            isInPartol = false;
        }


        if (enemyStateMachine.isFieldOfView(enemyStateMachine.player))
        {
            if(!enemyStateMachine.is_standby)
            enemyStateMachine.ChangeState(enemyStateMachine.states[typeof(EnemyChashingStatr)]);
        }


        // Vector3.Distance(enemyStateMachine.navMeshAgent.destination, enemyStateMachine.navMeshAgent.nextPosition) <= Mathf.Epsilon
    }

    public override void Exit()
    {
        base.Exit();
    }
    float RandomIdleTime()
    {
        return Random.Range(1f, 8f);
    }
    int RandomPoint()
    {
        return Random.Range(0, points.Count);
    }


}
