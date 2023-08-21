using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/EnemyEnemyStandByState", fileName = " EnemyStandByState")]
public class EnemyStandByState : EnemyBaseState
{
    private float standbytime;
    public override void Enter()
    {
        base.Enter();
        // Debug.Log("stand by" + standbytime);

        enemyStateMachine.animator.CrossFadeInFixedTime("Locomotion", 0.1f);

        standbytime = enemyStateMachine.standByTime;
        if (standbytime == 0)
        {
            //change to chashing state
            Debug.Log("change to chashing state");
            enemyStateMachine.ChangeState(enemyStateMachine.states[typeof(EnemyChashingStatr)]);

        }


    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        standbytime -= deltaTime;
        if (standbytime <= 0f)
        {
            //change to chashing state
            Debug.Log("change to chashing state");
            enemyStateMachine.ChangeState(enemyStateMachine.states[typeof(EnemyChashingStatr)]);

        }
    }

}
