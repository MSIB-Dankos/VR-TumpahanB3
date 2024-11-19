using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventBus), typeof(FlowObjective))]
public class AddScoreAfterFlow : MonoBehaviour
{
    public int addScore = 5;
    public AssesmenController assesmenController;
    public string eventAfterFlow = "OnEndFlow";

    private void Awake()
    {
        EventBus eventBus = GetComponent<EventBus>();
        eventBus.AddListener(eventAfterFlow, AddScore);
    }

    private void AddScore()
    {
        assesmenController.scoreController.AddScore(addScore);
    }
}
