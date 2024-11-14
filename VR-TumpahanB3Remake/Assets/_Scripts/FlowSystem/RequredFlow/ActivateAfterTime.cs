using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAfterTime : MonoBehaviour
{
    public float waitAfter = 5.0f;
    public List<GameObject> activatedObejcts = new List<GameObject>();
    public string eventBeforeFlow = "OnStartFlow";

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
        foreach (GameObject obj in activatedObejcts)
        {
            obj.SetActive(true);
        }
    }
}
