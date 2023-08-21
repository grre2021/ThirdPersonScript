using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class Parameter
{
    [Header("Component")]

    public Animator animator;

    public Rigidbody2D rigidbody2D;

    public Transform transform;
}


public class Core : MonoBehaviour
{
    private readonly List<CoreComponent> components = new List<CoreComponent>();


    public Parameter parameter;
    private void Awake()
    {
        var comps = GetComponentsInChildren<CoreComponent>();

        foreach (var comp in comps)
        {
            AddComponent(comp);
            Debug.Log("Get Component" + comp.name);
        }


        foreach (var comp in components)
        {
            comp.Init(this, parameter);
        }




        parameter.animator = GetComponentInParent<Animator>();
        parameter.rigidbody2D = GetComponentInParent<Rigidbody2D>();
        parameter.transform = GetComponentInParent<Transform>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    void AddComponent(CoreComponent component)
    {
        if (!components.Contains(component))
        {
            components.Add(component);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetCoreComponent<T>() where T : CoreComponent
    {
        //֪ʶ��
        var com = components.OfType<T>().FirstOrDefault();
        if (com == null)
        {
            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
        }

        return com;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public T GetCoreComponent<T>(ref T value) where T : CoreComponent
    {
        value = GetComponent<T>();
        return value;
    }

    private void Start()
    {

    }
}
