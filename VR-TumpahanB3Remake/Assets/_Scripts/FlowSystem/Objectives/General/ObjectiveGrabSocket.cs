using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectiveGrabSocket : FlowObjective
{
    public List<SocketInteractorAllowedObject> socketInteractorAllowedObjects;
    public List<FlowFilter> filters;

    [Header("Debug")]
    [ShowInInspector, ReadOnly] private int socketFilledCount = 0;

    private void OnEnable()
    {
        foreach (SocketInteractorAllowedObject socket in socketInteractorAllowedObjects)
        {
            socket.selectEntered.AddListener(SocketSelectEntered);
            socket.selectExited.AddListener(SocketSelectExited);
        }
    }

    private void OnDisable()
    {
        foreach (SocketInteractorAllowedObject socket in socketInteractorAllowedObjects)
        {
            socket.selectEntered.RemoveListener(SocketSelectEntered);
            socket.selectExited.AddListener(SocketSelectExited);
        }
    }

    private void SocketSelectEntered(SelectEnterEventArgs args)
    {
        socketFilledCount++;
    }

    private void SocketSelectExited(SelectExitEventArgs args)
    {
        socketFilledCount--;
    }

    public override bool IsFlowComplete()
    {
        if (filters.Count > 1)
        {
            foreach (FlowFilter flowFilter in filters)
            {
                if (!flowFilter.GetFilter())
                {
                    return false;
                }
            }
        }
        else if (filters.Count > 0)
        {
            if (!filters[0].GetFilter())
            {
                return false;
            }
        }
        return socketFilledCount >= socketInteractorAllowedObjects.Count;
    }
}
