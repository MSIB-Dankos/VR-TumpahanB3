using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketTelephoneFilter : FlowFilter
{
    public List<XRDirectInteractor> telephoneInteractor;
    public XRGrabInteractable telephoneInteractable;
    public VoiceLineQueue interupterVoiceLine;
    [field: SerializeField] public bool isTelephoneFlow { get; set; }

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip attentionClip;

    private Coroutine attentionRoutine;
    private bool isSelected = false;
    private void Awake()
    {
        telephoneInteractable.selectEntered.AddListener(args =>
        {
            if (args.interactableObject is XRDirectInteractor directInteractor)
            {
                if (!telephoneInteractor.Contains(directInteractor))
                {
                    return;
                }
            }
            else
            {
                return;
            }

            isSelected = true;
            if (isTelephoneFlow)
            {
                if (attentionRoutine != null)
                {
                    StopCoroutine(attentionRoutine);
                    attentionRoutine = null;
                    audioSource.Stop();
                }
            }
        });
        telephoneInteractable.selectExited.AddListener(args =>
        {
            if (args.interactableObject is XRDirectInteractor directInteractor)
            {
                if (!telephoneInteractor.Contains(directInteractor))
                {
                    return;
                }
            }
            else
            {
                return;
            }
            
            isSelected = false;
            if (isTelephoneFlow)
            {
                if (attentionRoutine != null)
                {
                    StopCoroutine(attentionRoutine);
                }
                attentionRoutine = StartCoroutine(GetTelephoneRoutine());
            }
        });
    }

    private IEnumerator GetTelephoneRoutine()
    {
        audioSource.clip = attentionClip;
        WaitUntil waitUntil = new WaitUntil(() => !audioSource.isPlaying);
        WaitForSeconds waitForSeconds = new WaitForSeconds(2.0f);
        while (true)
        {
            audioSource.Play();
            yield return waitUntil;
            yield return waitForSeconds;
        }
    }

    public override bool GetFilter()
    {
        return isSelected;
    }
}