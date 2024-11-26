using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class FeedbackCondition : MonoBehaviour
{
    [System.Serializable]
    public class Word
    {
        public enum WordMode
        {
            Reference,
            Manual
        }
        [HorizontalGroup(DisableAutomaticLabelWidth = true, LabelWidth = 50), LabelText("Word")] public WordMode wordMode;
        [HorizontalGroup(DisableAutomaticLabelWidth = true), HideLabel, ShowIf("@this.wordMode == WordMode.Manual")] public string word;
        [HorizontalGroup(DisableAutomaticLabelWidth = true), HideLabel, ShowIf("@this.wordMode == WordMode.Reference"), ValueDropdown("@$root.GetAssesmenList()")] public string wordReference;
    }

    [System.Serializable]
    public class FeedbackWord
    {
        public List<Word> words = new List<Word>();
    }

    public AssesmenController assesmenController;
    public List<FeedbackWord> feedbackWords = new List<FeedbackWord>();
    [ReadOnly] public FeedbackWord currentFeedbackWord = null;

    private ValueDropdownList<string> GetAssesmenList()
    {
        if (!assesmenController)
        {
            return new ValueDropdownList<string>() { };
        }

        ValueDropdownList<string> items = new ValueDropdownList<string>();
        foreach (AssesmenController.Objective item in assesmenController.objectives)
        {
            items.Add(item.objectiveName);
        }

        return items;
    }

    public virtual string GetFeebackWordString()
    {
        if (currentFeedbackWord == null)
        {
            return "";
        }

        string merge = "";
        foreach (Word word in currentFeedbackWord.words)
        {
            merge += word.wordMode == Word.WordMode.Manual ? word.word : word.wordReference;
        }
        return merge;
    }
}
