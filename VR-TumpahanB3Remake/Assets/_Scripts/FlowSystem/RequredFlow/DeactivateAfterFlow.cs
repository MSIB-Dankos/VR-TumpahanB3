using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterFlow : MonoBehaviour
{
    public List<GameObject> deactivatedObejcts = new List<GameObject>();
    public string eventAfterFlow = "OnEndFlow";

    private void Awake()
    {
        EventBus eventBus = GetComponent<EventBus>();
        eventBus.AddListener(eventAfterFlow, Deactivate);
    }

    private void Deactivate()
    {
        foreach (GameObject obj in deactivatedObejcts)
        {
            obj.SetActive(false);
        }
    }
}
