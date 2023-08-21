using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/EnemyDieState", fileName = "EnemyDieState")]
public class EnemyDieState :EnemyBaseState
{
    private float time;
    public override void Enter()
    {
        base.Enter();
        EventCenter.Instance.EventTrigger(eventEnemy.Die);
        time = 2f;
        enemyStateMachine.animator.CrossFadeInFixedTime("Die",0.1f);
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        time -= deltaTime;
        if (time < 0)
        {
            Destroy(enemyStateMachine.gameObject);
        }
    }
}
