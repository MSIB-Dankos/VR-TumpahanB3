using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class FillSocket : MonoBehaviour
{
    public List<SocketInteractorAllowedObject> socketInteractorAllowedObjects;
    public List<FlowFilter> filters;

    public UnityEvent onSocketFilled;

    [Header("Debug")]
    [ShowInInspector, ReadOnly] private int socketFilledCount = 0;

    private void OnEnable()
    {
        foreach (SocketInteractorAllowedObject socket in socketInteractorAllowedObjects)
        {
            socket.selectEntered.AddListener(SocketSelectEntered);
            socket.selectExited.AddListener(SocketSelectExited);
        }
        StartCoroutine(UpdateSocketFill());
    }

    private void OnDisable()
    {
        foreach (SocketInteractorAllowedObject socket in socketInteractorAllowedObjects)
        {
            socket.selectEntered.RemoveListener(SocketSelectEntered);
            socket.selectExited.AddListener(SocketSelectExited);
        }
        StopCoroutine(UpdateSocketFill());
    }

    private void SocketSelectEntered(SelectEnterEventArgs args)
    {
        socketFilledCount++;
    }

    private void SocketSelectExited(SelectExitEventArgs args)
    {
        socketFilledCount--;
    }

    private IEnumerator UpdateSocketFill()
    {
        bool socketFilled = false;
        WaitForSeconds updateTime = new WaitForSeconds(0.1f);
        while (!socketFilled)
        {
            if (IsSocketFilled())
            {
                onSocketFilled?.Invoke();
                socketFilled = true;
            }
            yield return updateTime;
        }
    }

    public bool IsSocketFilled()
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
