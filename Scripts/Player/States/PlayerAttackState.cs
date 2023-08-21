using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/PlayerAttackState", fileName = "PlayerAttackState")]
public class PlayerAttackState : PlayerBaseState
{

    float previousFrameTime;
    Attack attack;

    bool isAttack;

    public Transform target;

    public override void Enter()
    {
        parameter.trailRenderer.gameObject.SetActive(true);
        if (parameter.is_targeting)
        {
            target = parameter.targeter.currentTarget.trans;
            Transform transform = parameter.playerTr;
            //transform.LookAt(target);
        }

        attack = parameter.attacks[parameter.playerData.currrntCombatIndex];
        SoundManager.Instance.PlayOneShot(attack.attackClip);

        parameter.animator.CrossFadeInFixedTime(attack.animName, attack.transitionDraution);

        inputReader.Attack += IsAttack;

        //        Debug.Log("start attack");

    }



    public override void Tick(float deltaTime)
    {


        //float normalizedTime = GetNormalizedTime();
        //Debug.Log("no" + normalizedTime + "pre" + previousFrameTime);
        FaceTarget();

        previousFrameTime = GetNormalizedTime();


        if (previousFrameTime < attack.combaAttackTime) return;
        if (isAttack && attack.combaNextStateIndex != -1)
        {
            parameter.playerData.currrntCombatIndex += 1;
            //            Debug.Log(parameter.playerData.currrntCombatIndex);
            stateMachine.ChangeState(stateMachine.states[typeof(PlayerAttackState)]);
        }
        else
        {
            if (previousFrameTime > 0.9f)
            {
                ResetAttack();
                if (parameter.is_targeting)
                {
                    stateMachine.ChangeState(stateMachine.states[typeof(PlayerTargetingState)]);

                }
                else
                    stateMachine.ChangeState(stateMachine.states[typeof(PlayerFreeLookState)]);
            }
        }

    }

    public override void Exit()
    {
        parameter.trailRenderer.gameObject.SetActive(false);
        //Debug.Log("exit");
        if (attack.combaNextStateIndex == -1)
        {
            ResetAttack();
        }
        inputReader.Attack -= IsAttack;
        previousFrameTime = 0f;
        isAttack = false;

    }


    private void TryCombaAttack(float normalizedTime)
    {
        if (attack.combaAttackTime > normalizedTime) { return; }
        if (attack.combaNextStateIndex == -1) { return; }

    }

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = parameter.animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = parameter.animator.GetNextAnimatorStateInfo(0);

        if (parameter.animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;

        }
        else if (!parameter.animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;

        }
        else
            return 0f;
    }

    void IsAttack()
    {
        isAttack = true;
        inputReader.Attack -= IsAttack;
    }

    void ResetAttack() => parameter.playerData.currrntCombatIndex = 0;



}
