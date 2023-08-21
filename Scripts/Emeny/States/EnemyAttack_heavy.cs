using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/EnemyAttack_heavy", fileName = " EnemyState/EnemyAttack_heavy")]
public class EnemyAttack_heavy : EnemyBaseState
{
    [SerializeField] private List<Attack> heavyAttacks;

    [SerializeField] private List<Attack> farAttacks;
    [Range(0, 100)]
    [SerializeField] private int StandByRate;

    [SerializeField] private float lessStandByTime;
    [SerializeField] private float MaxStandByTime;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("attack");
        if (XZDistance(enemyStateMachine.transform, enemyStateMachine.player) >= 2.5f)
        {
            int attackIndex = Random.Range(0, farAttacks.Count);
            enemyStateMachine.animator.Play(farAttacks[attackIndex].animName);
            enemyStateMachine.currentAttack = farAttacks[attackIndex];
            SoundManager.Instance.PlayOneShot(farAttacks[attackIndex].attackClip);

        }
        else
        {
            //chose attack index
            int attackIndex = Random.Range(0, heavyAttacks.Count);
            //play the anim
            enemyStateMachine.animator.Play(heavyAttacks[attackIndex].animName);
            enemyStateMachine.currentAttack = heavyAttacks[attackIndex];
            SoundManager.Instance.PlayOneShot(heavyAttacks[attackIndex].attackClip);
        }


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
