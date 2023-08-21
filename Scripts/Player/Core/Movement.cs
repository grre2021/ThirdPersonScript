using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Movement : CoreComponent
{

    private Vector2 direction;




 /// <summary>
 /// 控制X方向上的移动
 /// </summary>
 /// <param name="velocity"></param>
   public void SetVelocityX(float velocity)
    {
        direction.Set(velocity, parameter.rigidbody2D.velocity.y);
        parameter.rigidbody2D.velocity = direction;
    }
    /// <summary>
    /// 控制Y方向上的移动
    /// </summary>
    /// <param name="velocity"></param>
    public void SetVelocityY(float velocity)
    {
        direction.Set(parameter.rigidbody2D.velocity.x, velocity);
        parameter.rigidbody2D.velocity = direction;
    }
}
