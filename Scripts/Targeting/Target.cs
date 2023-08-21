using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IAgent
{
    public Transform trans;
    private EnemyStateMachine enemyStateMachine;


    public void GetDamage(float damage, Vector3 direction, float force, float distanceAttacked)
    {
        enemyStateMachine.Hurt(damage, direction, force, distanceAttacked);
    }

    private void Start()
    {
        trans = GetComponent<Transform>();
        enemyStateMachine = GetComponent<EnemyStateMachine>();

    }

}
