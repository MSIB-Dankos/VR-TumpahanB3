using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveFindingObject : FlowObjective
{
    public GameObject targetObj;
    public RaycastObject raycastObject;

    public override bool IsFlowComplete()
    {
        bool find = false;
        raycastObject.CheckRaycast(() =>
        {
            if (raycastObject.GetRaycastHit().collider.gameObject == targetObj)
            {
                find = true;
            }
        });
        return find;
    }
}
