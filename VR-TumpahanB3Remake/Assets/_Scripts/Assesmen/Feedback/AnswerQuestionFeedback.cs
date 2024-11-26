using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class AnswerQuestionFeedback : FeedbackCondition
{
    [ValueDropdown("@this.GetAssesmenList()")] public string objectiveName;
    public List<Button> wrongAnswerButtons = new List<Button>();
    public List<Button> rightAnswerButtons = new List<Button>();

    private bool rightAnswer = false;

    private void Awake()
    {
        // Tidak menjawab
        currentFeedbackWord = feedbackWords[3];
        assesmenController.objectives.Find(x => x.objectiveName == objectiveName).onComplete.AddListener(CheckOnComplete);

        foreach (Button button in wrongAnswerButtons)
        {
            button.onClick.AddListener(SetWrongAnswer);
        }

        foreach (Button button in rightAnswerButtons)
        {
            button.onClick.AddListener(SetRightAnswer);
        }
    }

    private void CheckOnComplete(int objectiveIndex)
    {
        List<AssesmenController.Objective> objectives = assesmenController.objectives;
        if (objectiveIndex + 1 <= objectives.Count - 1)
        {
            if (objectives[objectiveIndex + 1].isComplete)
            {
                // Salah urutan
                currentFeedbackWord = feedbackWords[2];
                return;
            }
        }

        if (rightAnswer)
        {
            currentFeedbackWord = null;
        }
        else
        {
            // Salah jawaban
            currentFeedbackWord = feedbackWords[0];
        }
    }

    public void SetRightAnswer()
    {
        rightAnswer = true;
        currentFeedbackWord = null;
    }

    public void SetWrongAnswer()
    {
        rightAnswer = false;
        currentFeedbackWord = feedbackWords[0];
    }

    public void SetSeeJerryCan()
    {
        // cuma lihat jerry can
        currentFeedbackWord = feedbackWords[1];
    }
}
