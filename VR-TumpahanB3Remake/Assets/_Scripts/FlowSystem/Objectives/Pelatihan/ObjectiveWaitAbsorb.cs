using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ObjectiveWaitAbsorb : FlowObjective
{
    public GameObject absorbObject;
    public Vector3 absorbedScale;
    public float targetTimeInSecond;

    [ShowInInspector, ReadOnly] private float currentTargetScale = 0.0f;
    [ShowInInspector, ReadOnly] private float currentTime = 0.0f;
    [ShowInInspector, ReadOnly] private Vector3 startScale;

    private void Awake()
    {
        startScale = absorbObject.transform.localScale;
    }

    // Update per frame
    public override bool IsFlowComplete()
    {
        currentTime += Time.deltaTime;
        currentTargetScale = Mathf.Clamp01(currentTime / targetTimeInSecond);

        absorbObject.transform.localScale = Vector3.Lerp(startScale, absorbedScale, currentTargetScale);

        return currentTargetScale == 1.0f;
    }
}
