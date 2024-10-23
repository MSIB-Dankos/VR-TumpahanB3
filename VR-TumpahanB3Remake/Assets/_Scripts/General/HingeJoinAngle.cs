using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HingeJoint))]
public class HingeJoinAngle : MonoBehaviour
{
    public float targetAngle;
    public UnityEvent onTargetAngle;

    private HingeJoint joint;
    private void Start()
    {
        joint = GetComponent<HingeJoint>();
    }

    private void FixedUpdate()
    {
        if (joint.angle > targetAngle)
        {
            onTargetAngle?.Invoke();
        }
    }
}
