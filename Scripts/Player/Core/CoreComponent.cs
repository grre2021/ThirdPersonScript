using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    protected Core core;
    protected Parameter parameter;
    public virtual void Init(Core core, Parameter parameter)
    {
        this.core = core;
        this.parameter = parameter;
    }
}
