using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Mathematics;

public class WeaponDetection : Detection
{
    [Header("WeaponDetection")]
    public Transform startPoint;

    public Transform endPoint;

    public float radius;

    public LayerMask hitLayer;

    public bool debug;

    [Header("OverlapSphereDetection")]
    public float testSphereRadius;

    public Transform testPoint;

    [Header("Hit Parctice")] public GameObject hit;

    public void OnDrawGizmos()
    {
        if (debug && startPoint != null && endPoint != null)
        {
            Vector3 direction = endPoint.position - startPoint.position;
            float length = direction.magnitude;
            direction.Normalize();

            if (length > 0)
            {
                Gizmos.color = Color.yellow;

                Gizmos.DrawWireSphere(startPoint.position, radius);
                Gizmos.DrawWireSphere(endPoint.position, radius);

                Vector3 perpendicual = Vector3.Cross(direction, Vector3.up).normalized;

                Gizmos.DrawLine(startPoint.position + perpendicual * radius, endPoint.position + perpendicual * radius);
                Gizmos.DrawLine(startPoint.position - perpendicual * radius, endPoint.position - perpendicual * radius);

                perpendicual = Vector3.Cross(perpendicual, direction).normalized;

                Gizmos.DrawLine(startPoint.position + perpendicual * radius, endPoint.position + perpendicual * radius);
                Gizmos.DrawLine(startPoint.position - perpendicual * radius, endPoint.position - perpendicual * radius);
            }
        }
        
        if (debug && testPoint != null)
        {
            Gizmos.DrawSphere(testPoint.position, testSphereRadius);
        }
    }


    /// <summary>
    /// 检测函数，返回击中物体
    /// </summary>
    /// <returns></returns>
    public override List<Collider> GetDectection()
    {

        List<Collider> result = new List<Collider>();

        Collider[] hits = Physics.OverlapCapsule(startPoint.position, endPoint.position, radius, hitLayer);
        foreach (var item in hits)
        {

            if (targetTags.Contains(item.tag)
                && !wasHit.Contains(item.gameObject))
            {
                wasHit.Add(item.gameObject);
                Vector3 point= item.ClosestPoint(item.transform.position);
                GameObject _hit = Instantiate(this.hit);
                _hit.transform.position = point;
                _hit.transform.rotation=quaternion.identity;
                StartCoroutine(nameof(DestoryHit), _hit);
                result.Add(item);

                //Debug.Log("hit target");
            }
        }

        return result;
    }

    public override List<Collider> SphereDetection(Transform center, float radius)
    {
        List<Collider> result = new List<Collider>();
        Collider[] hits = Physics.OverlapSphere(center.position, radius, hitLayer);
        foreach (var item in hits)
        {
            if (targetTags.Contains(item.tag)
                && !wasHit.Contains(item.gameObject))
            {
                wasHit.Add(item.gameObject);
                result.Add(item);

                //Debug.Log("hit target");
            }

        }

        return result;
    }

    IEnumerator DestoryHit(GameObject hit)
    {
        yield return new WaitForSeconds(1f);
        Destroy(hit);
    }




}
