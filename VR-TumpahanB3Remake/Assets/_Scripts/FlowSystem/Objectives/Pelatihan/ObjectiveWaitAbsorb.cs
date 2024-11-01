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
        [HideInEditorMode, ReadOnly] public Vector3 startScale;
        [HideInEditorMode, ReadOnly] public bool isAbsorbDone;

        public Coroutine selectRoutine = null;
    }

    public float targetTimeInSecond;
    public List<AbsorbData> absorbDatas = new List<AbsorbData>();
    private bool isDone;
    private void Awake()
    {
        for (int i = 0; i < absorbDatas.Count; i++)
        {
            AbsorbData absorbData = absorbDatas[i];
            absorbData.startScale = absorbData.socketInteractorAllowedObject.transform.localScale;

            absorbData.socketInteractorAllowedObject.selectEntered.AddListener(args =>
            {
                if (absorbData.selectRoutine != null)
                {
                    return;
                }
                absorbData.selectRoutine = StartCoroutine(OnSelectEntered(absorbData));
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

    private IEnumerator OnSelectEntered(AbsorbData absorbData)
    {
        float currentTime = 0.0f;
        float value01 = 0.0f;
        while (value01 < 1.0f)
        {
            currentTime += Time.deltaTime;
            value01 = Mathf.Clamp01(currentTime / targetTimeInSecond);

            absorbData.socketInteractorAllowedObject.transform.localScale = Vector3.Lerp(absorbData.startScale, Vector3.zero, value01);
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
