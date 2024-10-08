using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [System.Flags]
    public enum FollowType
    {
        Position = 1 << 0,
        Rotation = 1 << 1,
        Scale = 1 << 2
    }

    public FollowType followType;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        if (followType.HasFlag(FollowType.Position))
        {
            transform.position = target.position;
        }

        if (followType.HasFlag(FollowType.Rotation))
        {
            transform.rotation = target.rotation;
        }

        if (followType.HasFlag(FollowType.Scale))
        {
            transform.localScale = target.localScale;
        }
    }
}
