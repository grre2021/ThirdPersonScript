using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : CoreComponent
{
    //[SerializeField] private Transform groundPosition;
    [Header("GroundDetection")]
    [SerializeField] private Transform groundTr;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsGround;

    public bool isGround()
    {
        //RaycastHit2D hit = Physics2D.Raycast(parameter.transform.position, transform.up, groundDetectionLength, groundLayer);
        if (Physics2D.OverlapCircle(groundTr.position, groundRadius, whatIsGround))
            return true;

        else
            return false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundTr.position, groundRadius);

    }
}
