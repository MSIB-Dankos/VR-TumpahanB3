using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveOpenBook : FlowObjective
{
    public Animator bookAnimator;
    public override bool IsFlowComplete()
    {
        return bookAnimator.GetFloat("Open") > 0.5f;
    }
}
