using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/EnemyAttack_test", fileName = " EnemyState/EnemyAttack_test")]
public class EnemyAttack_Test : EnemyBaseState
{
    [SerializeField] private List<Attack> attacks;
    [Range(0, 100)]
    [SerializeField] private int StandByRate;

    [SerializeField] private float lessStandByTime;
    [SerializeField] private float MaxStandByTime;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("attack");
        //chose attack index
        int attackIndex = Random.Range(0, attacks.Count);
        //play the anim
        enemyStateMachine.animator.Play(attacks[attackIndex].animName);


    }
    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        //Debug.Log(isFinishAttack());
        if (isFinishAttack())
        {
            if (isStandBy())
            {

                enemyStateMachine.standByTime = Random.Range(lessStandByTime, MaxStandByTime);


            }
            else
            {
                enemyStateMachine.standByTime = 0f;

            }
            enemyStateMachine.ChangeState(enemyStateMachine.states[typeof(EnemyStandByState)]);

        }


    }

    bool isStandBy()
    {
        if (rand(new int[] { StandByRate, 100 - StandByRate }, 100) == 0)
            return true;

        return false;

    }




}
