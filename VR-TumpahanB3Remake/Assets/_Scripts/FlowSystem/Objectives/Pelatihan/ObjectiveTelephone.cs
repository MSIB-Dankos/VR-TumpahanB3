using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveTelephone : FlowObjective
{
    public NumpadController numpadController;

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
        isCorrect = targetNumber == number;
    }

    public override bool IsFlowComplete()
    {   
        return isCorrect;
    }
}
