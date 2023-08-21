using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RootMotion : MonoBehaviour
{

    Animator animator;
    NavMeshAgent agent;

    private bool isChashing = false;



    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }
    private void OnAnimatorMove()
    {

        if (isChashing)
        {
            Vector3 rootPosition = animator.rootPosition;
            //讲agent y 值赋予到rootPosition
            rootPosition.y = agent.nextPosition.y;
            transform.position = rootPosition;
            agent.nextPosition = rootPosition;
        }
        else
        {
            animator.applyRootMotion = true;

            Vector3 rootPosition = animator.rootPosition;
            Quaternion rootRotation = animator.rootRotation;
            transform.position = rootPosition;
            transform.rotation = rootRotation;

            // Debug.Log("root motion true");

        }




    }

    public void chashingTrigger()
    {

        if (isChashing)
        {

            agent.updatePosition = false;
            isChashing = false;

        }
        else
        {
            agent.updatePosition = true;
            isChashing = true;

        }
        // Debug.Log("ischashin" + isChashing);

    }

}
