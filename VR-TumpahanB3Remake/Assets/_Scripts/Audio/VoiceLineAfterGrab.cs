using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VoiceLineAfterGrab : MonoBehaviour
{
    public VoiceLineQueue voiceLineQueue;
    public List<XRGrabInteractable> grabInteractables;
    [Header("Debug")]
    [ShowInInspector, ReadOnly] private bool isSocketAudio = false;

    private void OnEnable()
    {
        foreach (XRGrabInteractable grabInteractable in grabInteractables)
        {
            grabInteractable.selectEntered.AddListener(GrabSelectEntered);
        }
    }

    private void OnDisable()
    {
        foreach (XRGrabInteractable grabInteractable in grabInteractables)
        {
            grabInteractable.selectEntered.RemoveListener(GrabSelectEntered);
        }
    }

    private void GrabSelectEntered(SelectEnterEventArgs args)
    {
        if (isSocketAudio)
        {
            return;
        }

        voiceLineQueue.NextAudio();
        isSocketAudio = true;

        foreach (XRGrabInteractable grabInteractable in grabInteractables)
        {
            grabInteractable.selectEntered.RemoveListener(GrabSelectEntered);
        }
    }
}
