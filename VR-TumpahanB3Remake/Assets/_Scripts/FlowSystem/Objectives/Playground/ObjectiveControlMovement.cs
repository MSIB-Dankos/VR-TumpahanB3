using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectiveControlMovement : FlowObjective
{
    public InputActionReference inputMove;
    [ShowInInspector, ReadOnly] private bool isMoving;

    private void OnEnable()
    {
        inputMove.action.started += OnMove;
    }

    private void OnDisable()
    {
        inputMove.action.started -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        isMoving = true;
    }

    public override bool IsFlowComplete()
    {
        return isMoving;
    }
}
