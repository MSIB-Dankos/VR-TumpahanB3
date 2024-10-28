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

    public void RunAction(string actionName)
    {
        if (actionName == "")
        {
            return;
        }
        ActionEventMap actionMap = EventMaps[EventMaps.FindIndex(x => x.eventName == actionName)];
        actionMap.eventAction.Invoke();
    }

    public void AddListener(string actionName, System.Action action)
    {
        if (actionName == "")
        {
            return;
        }
        ActionEventMap actionMap = EventMaps[EventMaps.FindIndex(x => x.eventName == actionName)];
        actionMap.eventAction.AddListener(action.Invoke);
    }

    public void RemoveListener(string actionName, System.Action action)
    {
        if (actionName == "")
        {
            return;
        }
        ActionEventMap actionMap = EventMaps[EventMaps.FindIndex(x => x.eventName == actionName)];
        actionMap.eventAction.RemoveListener(action.Invoke);
    }
}
