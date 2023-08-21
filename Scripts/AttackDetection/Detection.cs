using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Detection : MonoBehaviour
{
    //使用列表存储目标
    public string[] targetTags;
    //使用列表存储目标
    public List<GameObject> wasHit = new List<GameObject>();

    public void ClearWasHit() => wasHit.Clear();

    public abstract List<Collider> GetDectection();

    public abstract List<Collider> SphereDetection(Transform center, float radius);



}
