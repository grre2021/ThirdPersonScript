using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    public enum NextPlayerMovement
    {
        jump,
        climbLow,
        climbHigh,
        vault
    }

    public NextPlayerMovement nextMovement = NextPlayerMovement.jump;

    public float lowClimbHeight = 0.5f;
    public float hightClimbHeight = 1.6f; 
    public float checkDistance = 1f;
    public LayerMask layerMask = default;
    
    public float climbAngle = 45f;
    public float bodyHeight = 1f;
    
    public float valutDistance = 0.2f; 
    
    private float climbDistance;
    private Vector3 ledge;    //墙壁边缘
    private Vector3 climbHitNormal;   //墙壁法线
    public Vector3 Ledge { get => ledge;}
    public Vector3 ClimbHitNormal { get => climbHitNormal;}

    public bool is_debug=false;

    public Transform test;
    private void Start()
    {
        climbDistance = Mathf.Cos(climbAngle) * checkDistance;

    }

    public NextPlayerMovement ClimbDetect(Transform playerTransform,Vector3 inputDirection,float offset)
    {
        float climbOffset = Mathf.Cos(climbAngle) * offset;
        Debug.Log("ClimbPddset"+climbOffset);
        //检查低位,检查高度是lowClimbHeight，检查距离是checkDistance
        if (Physics.Raycast(playerTransform.position + Vector3.up * lowClimbHeight, playerTransform.forward,out RaycastHit obsHit, checkDistance + offset))
        {
            climbHitNormal = obsHit.normal;
            Debug.Log("低位检测通过" + obsHit.normal);
            //(玩家朝向或者输入方向)角度不合要求（与墙面法线角度大于限制角度），玩家跳跃
            if (Vector3.Angle(-climbHitNormal, playerTransform.forward) > climbAngle || Vector3.Angle(-climbHitNormal, inputDirection) > climbAngle)
            {
                Debug.Log("角度不正");
                return NextPlayerMovement.jump;
            }

            //在墙壁法线方向再检测一次低位，检查距离是climbDistance
            if (Physics.Raycast(playerTransform.position + Vector3.up * lowClimbHeight, -climbHitNormal, out RaycastHit firstWallHit, climbDistance + climbOffset))
            {
                Debug.Log("低位法线方向检测通过" + firstWallHit.normal);
                test.position = firstWallHit.point;
                //向上提高一个身位bodyHeight，再检测一次
                if (Physics.Raycast(playerTransform.position + Vector3.up * (lowClimbHeight + bodyHeight), -climbHitNormal, out RaycastHit secondWallHit, climbDistance + climbOffset))
                {
                    Debug.Log("向上一个身位法线方向检测通过");
                    //向上提高两个身位bodyHeight，再检测一次
                    if (Physics.Raycast(playerTransform.position + Vector3.up * (lowClimbHeight + bodyHeight * 2), -climbHitNormal, out RaycastHit thirdWallHit, climbDistance + climbOffset))
                    {
                        Debug.Log("向上两个身位法线方向检测通过");
                        //向上提高三个身位bodyHeight，再检测一次，仍旧检测到障碍，玩家跳跃
                        if (Physics.Raycast(playerTransform.position + Vector3.up * (lowClimbHeight + bodyHeight * 3), - climbHitNormal, climbDistance + offset))
                        {
                            Debug.Log("太高了");
                            return NextPlayerMovement.jump;
                        }

                        //第三个身位没有检测到障碍，（从第二个身位向上一个身位的为止）向下检测，碰撞点即为墙边，玩家执行高位攀爬
                        else if (Physics.Raycast(thirdWallHit.point + Vector3.up * bodyHeight, Vector3.down, out RaycastHit ledgeHit, bodyHeight))
                        {
                            Debug.Log("在第二个身位上方检测到边缘");
                            ledge = ledgeHit.point;
                            return NextPlayerMovement.climbHigh;
                        }
                    }
                    //第二个身位没有检测到障碍，（从第一个身位向上一个身位的为止）向下检测，碰撞点即为墙边，玩家执行低位攀爬
                    else if (Physics.Raycast(secondWallHit.point + Vector3.up * bodyHeight, Vector3.down, out RaycastHit ledgeHit, bodyHeight))
                    {
                        Debug.Log("在第一个身位上方检测到边缘");
                        ledge = ledgeHit.point;
                        if (ledge.y - playerTransform.position.y > hightClimbHeight)
                        {
                            return NextPlayerMovement.climbHigh;
                        }
                        //处于低位攀爬高度，检查是否可以翻越，检测到宽度足够，则使用低位攀爬
                        else if (Physics.Raycast(secondWallHit.point + Vector3.up * bodyHeight - climbHitNormal * valutDistance, Vector3.down, bodyHeight))
                        {
                            return NextPlayerMovement.climbLow;
                        }
                        else
                        {
                            return NextPlayerMovement.vault;
                        }
                    }
                }
                else if (Physics.Raycast(firstWallHit.point + Vector3.up * bodyHeight, Vector3.down, out RaycastHit ledgeHit, bodyHeight))
                {
                    Debug.Log("在低位上方检测到边缘");
                    ledge = ledgeHit.point;
                    //处于低位攀爬高度，检查是否可以翻越，检测到宽度足够，则使用低位攀爬
                    if (Physics.Raycast(firstWallHit.point + Vector3.up * bodyHeight - climbHitNormal * valutDistance, Vector3.down, out ledgeHit, bodyHeight))
                    {
                        return NextPlayerMovement.climbLow;
                    }
                    else
                    {
                        return NextPlayerMovement.vault;
                    }
                }
            }
        }
        Debug.Log("啥也不是，跳吧");
        return NextPlayerMovement.jump;
    } 
    

    private void OnDrawGizmos()
    {
        if(!is_debug) return;
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(transform.position+Vector3.up*lowClimbHeight,(transform.position + Vector3.up * lowClimbHeight)+
                                                                     transform.forward*checkDistance);
        //
        Gizmos.color=Color.blue;
        
        Gizmos.DrawLine(transform.position + Vector3.up * lowClimbHeight,
            (transform.position + Vector3.up * lowClimbHeight)-climbHitNormal*checkDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(test.position + Vector3.up * bodyHeight - climbHitNormal * valutDistance,
            test.position + Vector3.up * bodyHeight - climbHitNormal * valutDistance + Vector3.down * bodyHeight);





    }
}
