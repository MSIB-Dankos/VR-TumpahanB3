using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionAfterTime : MonoBehaviour
{
    public float waitAfter = 5.0f;
    public string eventBeforeFlow = "OnStartFlow";
    public UnityEvent onAction;

    private void Awake()
    {
        EventBus eventBus = GetComponent<EventBus>();
        eventBus.AddListener(eventBeforeFlow, Activate);
    }

    private void Activate()
    {
        StartCoroutine(WaitToActivate());
    }

    private IEnumerator WaitToActivate()
    {
        yield return new WaitForSeconds(waitAfter);
        onAction?.Invoke();
    }
}
