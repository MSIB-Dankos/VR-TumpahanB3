using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectiveWaitAbsorb : FlowObjective
{
    [System.Serializable]
    public class AbsorbData
    {
        public SocketInteractorAllowedObject socketInteractorAllowedObject;
        public GameObject absorbedObject;
        [Header("Debug")]
        [HideInEditorMode, ReadOnly] public Vector3 startScale;
        [HideInEditorMode, ReadOnly] public bool isAbsorbDone;
        [HideInEditorMode, ReadOnly] public float currentTime = 0.0f;
        [HideInEditorMode, ReadOnly] public MeshRendererController currentKain;
        public Coroutine selectRoutine = null;
    }

    public float darkerKainAmount = 1.0f;
    public float targetTimeInSecond;
    public List<AbsorbData> absorbDatas = new List<AbsorbData>();
    private bool isDone;
    private void Awake()
    {
        for (int i = 0; i < absorbDatas.Count; i++)
        {
            AbsorbData absorbData = absorbDatas[i];
            absorbData.startScale = absorbData.absorbedObject.transform.localScale;

            absorbData.socketInteractorAllowedObject.selectEntered.AddListener(args =>
            {
                if (absorbData.selectRoutine != null)
                {
                    return;
                }
                if (args.interactableObject is XRGrabInteractable grabObj)
                {
                    absorbData.currentKain = grabObj.GetComponent<MeshRendererController>();
                }
                absorbData.selectRoutine = StartCoroutine(SelectRoutine(absorbData));
            });

            absorbData.socketInteractorAllowedObject.selectExited.AddListener(args =>
            {
                if (absorbData.selectRoutine != null)
                {
                    StopCoroutine(absorbData.selectRoutine);
                    absorbData.selectRoutine = null;
                }
            });
        }
    }

    private IEnumerator SelectRoutine(AbsorbData absorbData)
    {
        float value01 = 0.0f;

        Color colorKain = Color.black;
        bool hasKain = absorbData.currentKain;
        if (hasKain)
        {
            colorKain = absorbData.currentKain.meshRenderers[0].material.color;
        }

        while (value01 < 1.0f)
        {
            absorbData.currentTime += Time.deltaTime;
            value01 = Mathf.Clamp01(absorbData.currentTime / targetTimeInSecond);

            absorbData.absorbedObject.transform.localScale = Vector3.Lerp(absorbData.startScale, Vector3.zero, value01);
            if (hasKain)
            {
                float darkerAmount = darkerKainAmount * Time.deltaTime;
                colorKain = new Color(colorKain.r - darkerAmount, colorKain.g - darkerAmount, colorKain.b - darkerAmount, colorKain.a);
                absorbData.currentKain.SetColors(colorKain);
            }

            yield return null;
        }

        absorbData.isAbsorbDone = true;

        bool checkAbsorbDone = true;
        foreach (AbsorbData data in absorbDatas)
        {
            if (!data.isAbsorbDone)
            {
                checkAbsorbDone = false;
            }
        }

        isDone = checkAbsorbDone;
    }

    public override bool IsFlowComplete()
    {
        return isDone;
    }
}
