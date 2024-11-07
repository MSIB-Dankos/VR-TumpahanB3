using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFilter : FlowFilter
{
    public Button button;

    private bool isDone;
    public override bool GetFilter()
    {
        return isDone;
    }

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            isDone = true;
        });
    }
}
