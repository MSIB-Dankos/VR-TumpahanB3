using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class LinkedFlow : MonoBehaviour
{
    [InfoBox("Flow akan dikaitkan dengan menjalankannya secara berurutan, Flow Manager dijalankan dari atas ke bawah", InfoMessageType.Info)]
    public List<FlowManager> flowManagerList = new List<FlowManager>();

    [ShowInInspector, ReadOnly] private int currentActive = 0;

    private void Awake()
    {
        for (int i = 0; i < flowManagerList.Count; i++)
        {
            FlowManager flowManager = flowManagerList[i];
            flowManager.enabled = false;
            if (flowManager.flowList.Count > 0)
            {
                FlowManager.Flow lastFlow = GetLastFlowEnabled(flowManager);
                if (lastFlow != null)
                {
                    lastFlow.eventBus.AddListener(lastFlow.eventAfterFlow, NextFlowManager);
                }
            }
        }
        flowManagerList[currentActive].enabled = true;
    }

    private FlowManager.Flow GetLastFlowEnabled(FlowManager flowManager)
    {
        for (int i = 1; i == flowManager.flowList.Count; i++)
        {
            FlowManager.Flow lastFlow = flowManager.flowList[flowManager.flowList.Count - i];
            if (lastFlow.enable)
            {
                return lastFlow;
            }
            else
            {
                continue;
            }
        }

        return null;
    }

    private void NextFlowManager()
    {
        flowManagerList[currentActive].enabled = false;
        currentActive++;
        if (currentActive > flowManagerList.Count)
        {
            return;
        }
        flowManagerList[currentActive].enabled = true;
    }
}
