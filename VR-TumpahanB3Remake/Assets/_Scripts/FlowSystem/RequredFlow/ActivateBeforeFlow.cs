using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBeforeFlow : MonoBehaviour
{
    public List<GameObject> activatedObejcts = new List<GameObject>();
    public string eventBeforeFlow = "OnStartFlow";

    private void Awake()
    {
        EventBus eventBus = GetComponent<EventBus>();
        eventBus.AddListener(eventBeforeFlow, Activate);
    }

    private void Activate()
    {
        foreach (GameObject obj in activatedObejcts)
        {
            obj.SetActive(true);
        }
    }
}
