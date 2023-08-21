using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayerIsNear : MonoBehaviour
{
    public static bool playerIsNear { private set; get; }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter");
        playerIsNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerIsNear = false;
    }
    
}
