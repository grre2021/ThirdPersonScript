using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Data", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public float Hp;
    public float currentHp;
    public float freeLookWalkSpeed;
    public float freeLookRotationDamping;
    public float TargetingSpeed;

    public float turnSpeed;

    public int attackCombatIndex;

    public int currrntCombatIndex = 0;

    public float dodgeSpeed;

    public AudioClip audioClip;



}
