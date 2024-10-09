using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EventBus : SerializedMonoBehaviour
{
    public Dictionary<string, UnityEvent> ActionMap = new Dictionary<string, UnityEvent>();

    public void RunAction(string actionName)
    {
        if (actionName == "")
        {
            return;
        }
        ActionMap[actionName].Invoke();
    }
}
