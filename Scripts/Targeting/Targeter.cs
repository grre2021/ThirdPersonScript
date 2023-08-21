using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Targeter : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;

    public List<Target> targets;

    public Target currentTarget;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Target>(out Target target)) { targets.Add(target); }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Target>(out Target target)) { targets.Remove(target); }


    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) return false;
        else
        {
            Target closetTarget = null;
            float closestTargetDistance = Mathf.Infinity;
            foreach (Target target in targets)
            {
                Vector2 viewPos = mainCamera.WorldToViewportPoint(target.trans.position);
                if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
                {
                    continue;
                }

                Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);

                if (toCenter.sqrMagnitude < closestTargetDistance)
                {
                    closetTarget = target;
                    closestTargetDistance = toCenter.sqrMagnitude;
                }


            }
            currentTarget = closetTarget;
            cinemachineTargetGroup.AddMember(currentTarget.trans, 1, 1f);
            return true;
        }
    }

    public void Cancel()
    {
//        Debug.Log("targeter cancel");
        if (currentTarget == null) return;
        cinemachineTargetGroup.RemoveMember(currentTarget.trans.transform);
        currentTarget = null;

    }

}
