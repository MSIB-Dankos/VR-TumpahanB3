using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventBus), typeof(FlowObjective))]
public class AudioAfterFlowEnd : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public string eventAfterFlow = "OnEndFlow";

    private void Awake()
    {
        EventBus eventBus = GetComponent<EventBus>();
        eventBus.AddListener(eventAfterFlow, PlaySound);
    }

    private void PlaySound()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
