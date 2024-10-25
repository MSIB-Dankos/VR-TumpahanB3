using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour
{
    [System.Serializable]
    public class ActionEventMap
    {
        public string eventName;
        public UnityEvent eventAction;
    }

    [TableList] public List<ActionEventMap> EventMaps = new List<ActionEventMap>();

    private Dictionary<string, UnityEvent> ActionMap = new Dictionary<string, UnityEvent>();
    private void Awake()
    {
        foreach (ActionEventMap map in EventMaps)
        {
            ActionMap.Add(map.eventName, map.eventAction);
        }
    }

    public void RunAction(string actionName)
    {
        if (actionName == "")
        {
            return;
        }
        ActionMap[actionName].Invoke();
    }

    public void AddListener(string actionName, System.Action action)
    {
        if (actionName == "")
        {
            return;
        }
        ActionMap[actionName].AddListener(action.Invoke);
    }

    public void RemoveListener(string actionName, System.Action action)
    {
        if (actionName == "")
        {
            return;
        }
        ActionMap[actionName].RemoveListener(action.Invoke);
    }
}
