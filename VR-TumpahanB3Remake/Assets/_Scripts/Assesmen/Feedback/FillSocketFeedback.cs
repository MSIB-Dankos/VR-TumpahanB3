using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FillSocketFeedback : FeedbackCondition
{
    [ValueDropdown("@this.GetAssesmenList()")] public string objectiveName;

    [System.Serializable]
    public class SocketItem
    {
        [HorizontalGroup, HideLabel] public SocketInteractorAllowedObject socket;
        [HorizontalGroup, HideLabel] public string itemName;
        [ReadOnly] public bool filled;
    }

    public List<SocketItem> socketItems = new List<SocketItem>();

    private void Awake()
    {
        foreach (SocketItem socketItem in socketItems)
        {
            socketItem.socket.selectEntered.AddListener(args => socketItem.filled = true);
            socketItem.socket.selectExited.AddListener(args => socketItem.filled = false);
        }

        assesmenController.objectives.Find(x => x.objectiveName == objectiveName).onComplete.AddListener(CheckOnComplete);

        // tidak menyelesaikannya
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

    public override string GetFeebackWordString()
    {
        if (currentFeedbackWord == feedbackWords[0])
        {
            return base.GetFeebackWordString();
        }

        string filledSocket = "";
        string unfilledSocket = "";

        foreach (SocketItem socketItem in socketItems)
        {
            if (socketItem.filled)
            {
                filledSocket += string.Format($"{0}, ", socketItem.itemName);
            }
            else
            {
                unfilledSocket += string.Format($"{0}, ", socketItem.itemName);
            }
        }

        string mergeBase = base.GetFeebackWordString();
        if (filledSocket == "")
        {
            mergeBase = RemoveWordWithDollarSign(mergeBase);
            unfilledSocket.Remove(unfilledSocket.Length - 2);
        }
        if (unfilledSocket == "")
        {
            mergeBase = RemoveWordWithTagSign(mergeBase);
            filledSocket.Remove(filledSocket.Length - 2);
        }
        else
        {
            mergeBase.Replace("$", "");
            mergeBase.Replace("#", "");

            filledSocket.Remove(filledSocket.Length - 2);
            unfilledSocket.Remove(unfilledSocket.Length - 2);
        }

        mergeBase.Replace("{filledSocket}", filledSocket);
        mergeBase.Replace("{unfilledSocket}", unfilledSocket);

        return mergeBase;
    }

    private string RemoveWordWithDollarSign(string word)
    {
        word = System.Text.RegularExpressions.Regex.Replace(word, @"\$.*?\$", "").Trim();
        return word;
    }

    private string RemoveWordWithTagSign(string word)
    {
        word = System.Text.RegularExpressions.Regex.Replace(word, @"\#.*?\#", "").Trim();
        return word;
    }
}