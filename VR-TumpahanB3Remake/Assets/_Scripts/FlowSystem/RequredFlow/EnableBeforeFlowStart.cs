using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBeforeFlowStart : MonoBehaviour
{
    public List<Component> activatedObejcts = new List<Component>();
    public string eventBeforeFlow = "OnStartFlow";

    private void Awake()
    {
        EventBus eventBus = GetComponent<EventBus>();
        eventBus.AddListener(eventBeforeFlow, Enable);
    }

    private void Enable()
    {
        foreach (Component comp in activatedObejcts)
        {
            if (comp is Behaviour behaviour)
            {
                behaviour.enabled = true;
            }
            else if (comp is Collider collider)
            {
                collider.enabled = true;
            }
            else if (comp is Renderer renderer)
            {
                renderer.enabled = true;
            }
        }
    }
}
