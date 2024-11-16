using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssesmenController : MonoBehaviour
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
