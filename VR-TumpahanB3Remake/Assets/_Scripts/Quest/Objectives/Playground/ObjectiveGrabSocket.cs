using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectiveGrabSocket : FlowObjective
{
    public List<SocketInteractorAllowedObject> socketInteractorAllowedObjects;
    [ShowInInspector, ReadOnly] private int socketFilledCount = 0;
    private void OnEnable()
    {
        foreach (SocketInteractorAllowedObject socket in socketInteractorAllowedObjects)
        {
            socket.selectEntered.AddListener(SelectEntered);
            socket.selectExited.AddListener(SelectExited);
        }
    }

    private void OnDisable()
    {
        foreach (SocketInteractorAllowedObject socket in socketInteractorAllowedObjects)
        {
            socket.selectEntered.RemoveListener(SelectEntered);
            socket.selectExited.AddListener(SelectExited);
        }
    }

    private void SelectEntered(SelectEnterEventArgs args)
    {
        socketFilledCount++;
    }

    private void SelectExited(SelectExitEventArgs args)
    {
        socketFilledCount--;
    }

    public override bool IsFlowComplete()
    {
        return socketFilledCount >= socketInteractorAllowedObjects.Count;
    }
}
