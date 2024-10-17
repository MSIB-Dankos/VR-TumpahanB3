using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ObjectiveWait : FlowObjective
{
    public float waitTime;
    [ShowInInspector, ReadOnly] private float currentTime;
    public override bool IsFlowComplete()
    {
        currentTime += Time.deltaTime;
        return currentTime > waitTime;
    }
}
