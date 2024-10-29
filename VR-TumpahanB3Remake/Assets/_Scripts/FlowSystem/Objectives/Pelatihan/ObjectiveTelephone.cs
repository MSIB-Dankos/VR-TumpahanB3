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

        IEnumerator CallTextAnimateRoutine()
        {
            audioSource.Play();
            numpadController.Clear();
            numpadController.Disable();
            WaitForSeconds textDotWait = new WaitForSeconds(0.25f);

            for (int i = 0; i < 10; i++)
            {
                numpadController.AddNumber(".");
                if (i % 3 == 0)
                {
                    numpadController.Clear();
                }
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
        }
    }

    public override bool IsFlowComplete()
    {
        return isCorrect;
    }
}
