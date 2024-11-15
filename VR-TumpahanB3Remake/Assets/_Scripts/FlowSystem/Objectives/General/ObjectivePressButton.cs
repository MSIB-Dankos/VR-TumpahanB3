using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivePressButton : FlowObjective
{
    public Button pressButton;
    public List<FlowFilter> filters = new List<FlowFilter>();

    private bool isDone;

    private void Awake()
    {
        pressButton.onClick.AddListener(() => isDone = true);
    }

    public override bool IsFlowComplete()
    {
        if (filters.Count > 1)
        {
            foreach (FlowFilter flowFilter in filters)
            {
                if (!flowFilter.GetFilter())
                {
                    return false;
                }
            }
        }
        else if (filters.Count > 0)
        {
            if (!filters[0].GetFilter())
            {
                return false;
            }
        }
        return isDone;
    }
}
