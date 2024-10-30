using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ObjectiveWaitAbsorb : FlowObjective
{
    public GameObject absorbObject;
    public Vector3 absorbedScale;
    public float targetTimeInSecond;

    [Header("Mesh Color")]
    public MeshRendererController meshRendererController;
    public Color endColor;

    [ShowInInspector, ReadOnly] private float value01 = 0.0f;
    [ShowInInspector, ReadOnly] private float currentTime = 0.0f;
    [ShowInInspector, ReadOnly] private Vector3 startScale;
    [ShowInInspector, ReadOnly] private Color startColor;
    private void Awake()
    {
        startScale = absorbObject.transform.localScale;
        startColor = meshRendererController.meshRenderers[0].material.color;
    }

    public override bool IsFlowComplete()
    {
        currentTime += Time.deltaTime;
        value01 = Mathf.Clamp01(currentTime / targetTimeInSecond);

        absorbObject.transform.localScale = Vector3.Lerp(startScale, absorbedScale, value01);
        meshRendererController.SetColors(Color.Lerp(startColor, endColor, value01));

        return value01 == 1.0f;
    }
}
