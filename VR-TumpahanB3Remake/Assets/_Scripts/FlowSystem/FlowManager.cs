using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FlowManager : MonoBehaviour
{
    [System.Serializable]
    public class Flow
    {
        [Required] public FlowObjective flowObjective;
        public EventBus eventBus;
        [HideIf("@this.eventBus == null")] public string eventBeforeFlow = "OnStartFlow";
        [HideIf("@this.eventBus == null")] public string eventAfterFlow = "OnEndFlow";
    }

    public List<Flow> flowList = new List<Flow>();
    [ShowInInspector, ReadOnly] private int currentFlow = -1;

    private void Start()
    {
        SetFlow(0);
    }

    private void Update()
    {
        if (currentFlow != -1)
        {
            if (flowList[currentFlow].flowObjective.IsFlowComplete())
            {
                SetFlow(currentFlow + 1 >= flowList.Count ? -1 : currentFlow + 1);
            }
        }
    }

    private void SetFlow(int index)
    {
        if (currentFlow != -1)
        {
            RunCurrentEventBusFlow(flowList[currentFlow].eventAfterFlow);
        }
        if (index == -1)
        {
            currentFlow = -1;
            return;
        }
        currentFlow = index;
        RunCurrentEventBusFlow(flowList[currentFlow].eventBeforeFlow);
    }

    private void RunCurrentEventBusFlow(string actionName)
    {
        if (flowList[currentFlow].eventBus)
        {
            flowList[currentFlow].eventBus.RunAction(actionName);
        }
    }
}