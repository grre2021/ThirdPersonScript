using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Data/New PlayerData",fileName ="New PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]

    public float walkSpeed;

    [Header("Jump")]
    public int jumpCount;
    public float jumpForce;
}
