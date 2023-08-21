using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通过继承这个接口，来编写击中物体的函数
/// </summary>
public interface IAgent
{
    public void GetDamage(float damage, Vector3 direction, float force, float distanceAttacked);


}


