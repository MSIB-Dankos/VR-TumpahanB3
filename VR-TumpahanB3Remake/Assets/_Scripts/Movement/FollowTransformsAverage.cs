using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransformsAverage : MonoBehaviour
{
    [System.Flags]
    public enum FollowType
    {
        Position = 1 << 0,
        Rotation = 1 << 1,
        Scale = 1 << 2
    }

    public FollowType followType;
    public List<Transform> targets;

    private Vector3 targetPos, targetScale;
    private Quaternion targetRot;

    // Update is called once per frame
    void Update()
    {
        float weight = 1.0f / (float)targets.Count;
        
        targetPos = Vector3.zero;
        targetRot = Quaternion.identity;
        targetScale = transform.localScale;
        
        for (int i = 0; i < targets.Count; i++)
        {
            Transform target = targets[i];
            if (!target)
            {
                continue;
            }

            if (followType.HasFlag(FollowType.Position))
            {
                targetPos += target.position;
            }

            if (followType.HasFlag(FollowType.Rotation))
            {
                targetRot *= Quaternion.Slerp(Quaternion.identity, target.rotation, weight);
            }

            if (followType.HasFlag(FollowType.Scale))
            {
                targetScale += target.localScale;
            }
        }

        targetPos /= targets.Count;
        targetScale /= targets.Count;

        transform.position = targetPos;
        transform.rotation = targetRot;
        transform.localScale = targetScale;
    }
}
