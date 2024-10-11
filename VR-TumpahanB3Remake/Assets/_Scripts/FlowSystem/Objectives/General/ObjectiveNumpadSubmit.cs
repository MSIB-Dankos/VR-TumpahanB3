using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ObjectiveNumpadSubmit : FlowObjective
{
    public string inputTarget;
    public NumpadController numpadController;
    
    [ShowInInspector, ReadOnly] private bool isSubmitTarget = false;

    private void OnEnable()
    {
        numpadController.OnSubmit.AddListener(CheckNumpadSubmit);
    }

    private void OnDisable()
    {
        numpadController.OnSubmit.RemoveListener(CheckNumpadSubmit);
    }

    private void CheckNumpadSubmit(string input)
    {
        if (input == inputTarget)
        {
            isSubmitTarget = true;
        }
    }

    public override bool IsFlowComplete()
    {
        return isSubmitTarget;
    }
}
