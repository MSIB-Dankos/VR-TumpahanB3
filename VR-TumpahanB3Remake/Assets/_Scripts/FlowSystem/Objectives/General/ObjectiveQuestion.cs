using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ObjectiveQuestion : FlowObjective
{
    public List<Button> wrongAnswerButtons;
    public List<Button> rightAnswerButtons;
    public bool isAssesmen;

    public UnityEvent onWrongAnswer;

    private bool isDone;
    private void Awake()
    {
        foreach (Button button in wrongAnswerButtons)
        {
            button.onClick.AddListener(OnWrongAnswer);
        }

        foreach (Button button in rightAnswerButtons)
        {
            button.onClick.AddListener(OnRightAnswer);
        }
    }

    private void OnWrongAnswer()
    {
        if (isAssesmen)
        {
            isDone = true;
            return;
        }
        onWrongAnswer?.Invoke();
    }

    private void OnRightAnswer()
    {
        isDone = true;
    }

    public override bool IsFlowComplete()
    {
        return isDone;
    }
}
