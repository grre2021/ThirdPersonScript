using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/EnemyAttackCenterState", fileName = " EnemyAttackCenterState")]
public class EnemyAttackCenterState : EnemyBaseState
{
    [Range(0, 100)]
    [SerializeField] int lightAttackTypeRate;

    [Range(0, 100)]
    [SerializeField] int heavyAttackTypeRate;
    private AnimatorStateInfo animatorStateInfo;
    State currentAttackType;

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("enter attack center");
        animatorStateInfo = enemyStateMachine.animator.GetCurrentAnimatorStateInfo(0);
        Attack();
        enemyStateMachine.ChangeState(currentAttackType);
        enemyStateMachine.transform.LookAt(enemyStateMachine.player);
        

    }


    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);

    }

    public override void Exit()
    {
        base.Exit();
        
    }



    public void Attack()
    {
        int attackIndex = rand(new int[] { lightAttackTypeRate, heavyAttackTypeRate }, lightAttackTypeRate + heavyAttackTypeRate);
        currentAttackType = enemyStateMachine.attacktypes[attackIndex];
    }




}
