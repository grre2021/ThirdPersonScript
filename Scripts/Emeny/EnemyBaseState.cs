using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : ScriptableObject, State
{
    protected EnemyStateMachine enemyStateMachine;



    public void Initialize(EnemyStateMachine enemyStateMachine)
    {
        this.enemyStateMachine = enemyStateMachine;

    }

    public virtual void Enter()
    {
        //enemyStateMachine.hurtingEvent.AddListener(Hurt);

    }

    public virtual void Tick(float deltaTime)
    {
        // Debug.Log(rand(new int[] { 40, 60 }, 100));

    }

    public virtual void Exit()
    {
        //enemyStateMachine.hurtingEvent.RemoveListener(Hurt);

    }
    public int rand(int[] rate, int total)
    {
        int randomValue = Random.Range(1, total + 1);
        int a = 0;
        for (int i = 0; i < rate.Length; i++)
        {
            a += rate[i];
            if (randomValue <= a)
            {
                return i;
            }
        }
        return -1;

    }
    public bool isFinishAnim()
    {
        AnimatorStateInfo currentStateInfo = enemyStateMachine.animator.GetCurrentAnimatorStateInfo(0);
        float currentNormalizedTime = currentStateInfo.normalizedTime;
        //Debug.Log("currentNormalizedTime  " + currentNormalizedTime);

        //Debug.Log(currentNormalizedTime);

        if (currentNormalizedTime >= 0.98f)
        {
            //Debug.Log("finish attack");
            return true;
        }
        return false;
    }

    public bool isFinishAttack()
    {
        AnimatorStateInfo currentStateInfo = enemyStateMachine.animator.GetCurrentAnimatorStateInfo(0);
        float currentNormalizedTime = currentStateInfo.normalizedTime;
        //Debug.Log("currentNormalizedTime  " + currentNormalizedTime);

        //Debug.Log(currentNormalizedTime);

        if (currentNormalizedTime >= 0.98f && currentStateInfo.IsTag("Attack"))
        {
            //Debug.Log("finish attack");
            return true;
        }
        return false;
    }

    public float XZDistance(Transform a, Transform b)
    {
        return Vector3.Distance(
            new Vector3(a.position.x, 0, a.position.z), new Vector3(b.position.x, 0, b.position.z)
        );
    }



}
