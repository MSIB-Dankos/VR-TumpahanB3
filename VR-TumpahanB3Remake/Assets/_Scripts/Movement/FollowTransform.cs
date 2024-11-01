using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
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

    [System.Flags]
    public enum Axis
    {
        X = 1 << 0,
        Y = 1 << 1,
        Z = 1 << 2
    }

    public FollowType followType;
    public Transform target;

    [Title("Axis"), ShowInInspector, HideIf("@this.followType == (FollowType)0"), HideLabel, CustomValueDrawer("")] private int inspectorLine;
    [ShowIf("@this.followType.HasFlag(FollowType.Position)")] public Axis positionAxis = Axis.X | Axis.Y | Axis.Z;
    [ShowIf("@this.followType.HasFlag(FollowType.Rotation)")] public Axis rotationAxis = Axis.X | Axis.Y | Axis.Z;
    [ShowIf("@this.followType.HasFlag(FollowType.Scale)")] public Axis scaleAxis = Axis.X | Axis.Y | Axis.Z;

    // Update is called once per frame
    void Update()
    {
        if (!target)
        {
            return;
        }

        if (followType.HasFlag(FollowType.Position))
        {
            Vector3 targetPosition = Vector3.zero;

            targetPosition.x = positionAxis.HasFlag(Axis.X) ? target.position.x : transform.position.x;
            targetPosition.y = positionAxis.HasFlag(Axis.Y) ? target.position.y : transform.position.y;
            targetPosition.z = positionAxis.HasFlag(Axis.Z) ? target.position.z : transform.position.z;

            transform.position = targetPosition;
        }

        if (followType.HasFlag(FollowType.Rotation))
        {
            Vector3 targetEuler = Vector3.zero;

            targetEuler.x = rotationAxis.HasFlag(Axis.X) ? target.eulerAngles.x : transform.eulerAngles.x;
            targetEuler.y = rotationAxis.HasFlag(Axis.Y) ? target.eulerAngles.y : transform.eulerAngles.y;
            targetEuler.z = rotationAxis.HasFlag(Axis.Z) ? target.eulerAngles.z : transform.eulerAngles.z;

            transform.rotation = Quaternion.Euler(targetEuler);
        }

        if (followType.HasFlag(FollowType.Scale))
        {
            Vector3 targetScale = Vector3.zero;

            targetScale.x = scaleAxis.HasFlag(Axis.X) ? target.localScale.x : transform.localScale.x;
            targetScale.y = scaleAxis.HasFlag(Axis.Y) ? target.localScale.y : transform.localScale.y;
            targetScale.z = scaleAxis.HasFlag(Axis.Z) ? target.localScale.z : transform.localScale.z;

            transform.localScale = targetScale;
        }
    }
}
