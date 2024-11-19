using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

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
    }

    public ScoreController scoreController;
    [ListDrawerSettings(NumberOfItemsPerPage = 5)] public List<Objective> objectives = new List<Objective>();

    public void UpdateCompleteObjective(string objectiveName)
    {
        Objective objective = objectives.Find(x => x.objectiveName == objectiveName);
        if (objective == null)
        {
            Debug.LogError("No Objective Found");
            return;
        }

        int objectiveIndex = objectives.IndexOf(objective);

        if (objectiveIndex + 1 <= objectives.Count - 1)
        {
            if (objectives[objectiveIndex + 1].isComplete)
            {
                return;
            }
        }

        objective.isComplete = true;
        scoreController.AddScore(objective.score);
    }
}
