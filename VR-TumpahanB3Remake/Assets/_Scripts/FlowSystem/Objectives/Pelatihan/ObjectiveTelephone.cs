using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveTelephone : FlowObjective
{
    public NumpadController numpadController;

    [Header("Audio")]
    public AudioSource audioSource;
    public float callSequenceTime = 1.0f;
    public float callSequenceStop = 1.0f;

    [Header("Events")]
    public UnityEvent onFailed;

    [Header("Debug")]
    [ShowInInspector, ReadOnly] private bool isCorrect;
    [ShowInInspector, ReadOnly] private string targetNumber => PKKTeamNumber.siteNumbers[PKKTeamNumber.currentSite];

    private void OnEnable()
    {
        numpadController.OnSubmit.AddListener(OnCall);
    }

    private void OnDisable()
    {
        numpadController.OnSubmit.RemoveListener(OnCall);
    }

    private void OnCall(string number)
    {
        StartCoroutine(CallTextAnimateRoutine());
        StartCoroutine(CallAudio());

        IEnumerator CallAudio()
        {
            WaitForSeconds sequenceTime = new WaitForSeconds(callSequenceTime);
            WaitForSeconds sequenceStop = new WaitForSeconds(callSequenceStop);
            while (true)
            {
                audioSource.Play();
                yield return sequenceTime;
                audioSource.Stop();
                yield return sequenceStop;
            }
        }

        IEnumerator CallTextAnimateRoutine()
        {
            numpadController.Clear();
            numpadController.Disable();

            WaitForSeconds textDotWait = new WaitForSeconds(0.25f);

            for (int i = 0; i < 10; i++)
            {
                //if (!audioSource.isPlaying) audioSource.Play();
                numpadController.AddNumber(".");
                if (i % 3 == 0)
                {
                    numpadController.Clear();
                }

                //if (i % 4 == 0) audioSource.Stop();
                yield return textDotWait;
            }
            numpadController.Clear();

            isCorrect = targetNumber == number;
            if (!isCorrect)
            {
                onFailed?.Invoke();
            }

            numpadController.Enable();
            audioSource.Stop();

            StopCoroutine(CallAudio());
        }
    }

    public override bool IsFlowComplete()
    {
        return isCorrect;
    }
}
