using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBoxAgent : MonoBehaviour, IAgent
{
    PlayerStateMachine playerStateMachine;
    public void GetDamage(float damage, Vector3 direction, float force, float distanceAttacked)
    {
        Debug.Log("hit player");
        playerStateMachine.Hurt(damage,direction,force,distanceAttacked);

    }
    private void Start()
    {
        playerStateMachine = GetComponent<PlayerStateMachine>();
        
    }
}
