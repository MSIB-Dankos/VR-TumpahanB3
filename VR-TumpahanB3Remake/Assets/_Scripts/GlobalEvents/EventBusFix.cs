using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class EventBusFix : MonoBehaviour
{
    // [Button]
    // public void SetAllActionMapIntoEventMap()
    // {
    //     EventBus[] objs = FindObjectsByType<EventBus>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
    //     if (objs.Length > 0)
    //     {
    //         foreach (EventBus eventBus in objs)
    //         {
    //             eventBus.EventMaps = new List<EventBus.ActionEventMap>();
    //             foreach (var action in eventBus.ActionMap)
    //             {
    //                 eventBus.EventMaps.Add(new EventBus.ActionEventMap()
    //                 {
    //                     eventName = action.Key,
    //                     eventAction = action.Value
    //                 });
    //             }
    //         }
    //     }
    // }
}
