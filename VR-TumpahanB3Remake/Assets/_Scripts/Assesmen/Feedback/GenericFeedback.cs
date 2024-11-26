using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GenericFeedback : FeedbackCondition
{
    [ValueDropdown("@this.GetAssesmenList()")] public string objectiveName;

    private void Awake()
    {
        assesmenController.objectives.Find(x => x.objectiveName == objectiveName).onComplete.AddListener(CheckOnComplete);

        currentFeedbackWord = feedbackWords[1];
    }

    private void CheckOnComplete(int objectiveIndex)
    {
        List<AssesmenController.Objective> objectives = assesmenController.objectives;
        if (objectiveIndex + 1 <= objectives.Count - 1)
        {
            if (objectives[objectiveIndex + 1].isComplete)
            {
                // Salah urutan
                currentFeedbackWord = feedbackWords[0];
                return;
            }
        }

        currentFeedbackWord = null;
    }
}
