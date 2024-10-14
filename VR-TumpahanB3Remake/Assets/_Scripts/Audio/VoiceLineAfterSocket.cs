using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VoiceLineAfterSocket : MonoBehaviour
{
    public VoiceLineQueue voiceLineQueue;
    public List<SocketInteractorAllowedObject> grabInteractables;
    [Header("Debug")]
    [ShowInInspector, ReadOnly] private bool isSocketAudio = false;

    private void OnEnable()
    {
        foreach (SocketInteractorAllowedObject grabInteractable in grabInteractables)
        {
            grabInteractable.selectEntered.AddListener(SocketSelectEntered);
        }
        foreach (SocketInteractorAllowedObject grabInteractable in grabInteractables)
        {
            grabInteractable.selectEntered.AddListener(SocketSelectEntered);
        }
    }

    private void OnDisable()
    {
        foreach (SocketInteractorAllowedObject grabInteractable in grabInteractables)
        {
            grabInteractable.selectEntered.RemoveListener(SocketSelectEntered);
        }
    }

    private void SocketSelectEntered(SelectEnterEventArgs args)
    {
        if (isSocketAudio)
        {
            return;
        }

        voiceLineQueue.NextAudio();
        isSocketAudio = true;

        foreach (SocketInteractorAllowedObject grabInteractable in grabInteractables)
        {
            grabInteractable.selectEntered.RemoveListener(SocketSelectEntered);
        }
    }
}
