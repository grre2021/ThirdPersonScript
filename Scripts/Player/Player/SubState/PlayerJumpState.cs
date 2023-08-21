using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]
public class PlayerJumpState : PlayerAbilityState
{
    /// <summary>
    /// Ê£ÓàÌøÔ¾´ÎÊı
    /// </summary>
    private int jumpLeft;
    public override void Enter()
    {
        base.Enter();
        movement.SetVelocityY(playerData.jumpForce);
        isAbilityDone = true;
    }


    public bool isAllowJump ()
    {
        if(jumpLeft>0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ResetJumpCount() => jumpLeft = playerData.jumpCount;
}
