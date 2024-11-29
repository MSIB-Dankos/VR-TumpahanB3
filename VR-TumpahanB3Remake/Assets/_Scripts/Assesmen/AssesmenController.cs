using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AssesmenController : MonoBehaviour
{
    [System.Serializable]
    public class ScoreController
    {
        [Header("Score")]
        public int currentScore;

        [Header("UI")]
        public TMP_Text scoreText;

        public void AddScore(int addScore)
        {
            currentScore += addScore;
            UpdateUI();
        }

        public void UpdateUI()
        {
            scoreText.text = "Nilai : " + currentScore;
        }
    }

    [System.Serializable]
    public class Objective
    {
        [HorizontalGroup(GroupID = "0"), HideLabel] public string objectiveName;
        [HorizontalGroup(GroupID = "1", DisableAutomaticLabelWidth = true), ReadOnly] public bool isComplete;
        [HorizontalGroup(GroupID = "1", DisableAutomaticLabelWidth = true)] public int score;

        [HideInInspector] public UnityEvent<int> onComplete;
    }

    public ScoreController scoreController;
    [ListDrawerSettings(NumberOfItemsPerPage = 5)] public List<Objective> objectives = new List<Objective>();
    [Header("Feedbacks")]
    public List<FeedbackCondition> feedbackConditions = new List<FeedbackCondition>();
    public TMP_Text listTextPrefab;
    public RectTransform listTextContainer;

    private List<TMP_Text> generatedFeedbacks = new List<TMP_Text>();

    public void UpdateCompleteObjective(string objectiveName)
    {
        Objective objective = objectives.Find(x => x.objectiveName == objectiveName);
        if (objective == null)
        {
            Debug.LogError($"No Objective Found: {objectiveName}");
            return;
        }

        int objectiveIndex = objectives.IndexOf(objective);

        if (objectiveIndex + 1 <= objectives.Count - 1)
        {
            if (objectives[objectiveIndex + 1].isComplete)
            {
                objective.isComplete = true;
                objective.onComplete?.Invoke(objectiveIndex);
                return;
            }
        }

        if (!objective.isComplete)
        {
            objective.isComplete = true;
            scoreController.AddScore(objective.score);
            objective.onComplete?.Invoke(objectiveIndex);
        }
    }

    public void MakeFeedbacks()
    {
        foreach (FeedbackCondition feedbackCondition in feedbackConditions)
        {
            if (feedbackCondition.currentFeedbackWord != null)
            {
                TMP_Text feedbackText = Instantiate(listTextPrefab, listTextContainer);
                feedbackText.text = feedbackCondition.GetFeebackWordString();
                generatedFeedbacks.Add(feedbackText);
            }
        }
    }
}
