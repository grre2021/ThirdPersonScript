using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Attack
{
    //攻击动画的名称

    [field: SerializeField] public string animName { get; private set; }
    [field: SerializeField] public float transitionDraution { get; private set; }
    [field: SerializeField] public int combaNextStateIndex = -1;
    [field: SerializeField] public float combaAttackTime { get; private set; }

    [field: SerializeField] public float Force { get; private set; }
    [field: SerializeField] public float damage { get; private set; }

    [field: SerializeField] public float distanceAttacked { get; private set; }
    
    [field: SerializeField] public AudioClip attackClip { get; private set; }

}
