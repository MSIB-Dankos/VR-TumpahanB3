using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ObjectiveWait : FlowObjective
{
    public float waitTime;
    public List<FlowFilter> filters = new List<FlowFilter>();

    [ShowInInspector, ReadOnly] private float currentTime;
    public override bool IsFlowComplete()
    {
        if (filters.Count > 1)
        {
            foreach (FlowFilter flowFilter in filters)
            {
                if (!flowFilter.GetFilter())
                {
                    return false;
                }
            }
        }
        else if (filters.Count > 0)
        {
            if (!filters[0].GetFilter())
            {
                return false;
            }
        }

        currentTime += Time.deltaTime;
        return currentTime > waitTime;
    }
}
