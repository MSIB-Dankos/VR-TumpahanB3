using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LinkedFlow : MonoBehaviour
{
    [InfoBox("Flow akan dikaitkan dengan menjalankannya secara berurutan, Flow Manager dijalankan dari atas ke bawah", InfoMessageType.Info)]
    public List<FlowManager> flowManagerList = new List<FlowManager>();

    private int currentActive = 0;

    private void Awake()
    {
        for (int i = 0; i < flowManagerList.Count; i++)
        {
            FlowManager flowManager = flowManagerList[i];
            flowManager.enabled = false;
            if (flowManager.flowList.Count > 0)
            {
                FlowManager.Flow lastFlow = flowManager.flowList[flowManager.flowList.Count - 1];
                lastFlow.eventBus.AddListener(lastFlow.eventAfterFlow, NextFlowManager);
            }
        }
        flowManagerList[currentActive].enabled = true;
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
