using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class BookOpen : MonoBehaviour
{
    public XRGrabInteractable xrGrabInteractable;
    public Animator animator;
    public float tresholdDistance = 1.0f;

    private Transform firstInteractor, secondInteractor;

    private void OnEnable()
    {
        xrGrabInteractable.selectEntered.AddListener(OnSelectEntered);
        xrGrabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        xrGrabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        xrGrabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void Update()
    {
        if (secondInteractor && firstInteractor)
        {
            float dist = Vector3.Distance(firstInteractor.position, secondInteractor.position);
            dist = dist > tresholdDistance ? tresholdDistance : dist;
            animator.SetFloat("Open", dist / tresholdDistance);
        }
        else
        {
            animator.SetFloat("Open", 0.0f);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!firstInteractor)
        {
            firstInteractor = args.interactorObject.transform;
        }
        else
        {
            secondInteractor = args.interactorObject.transform;
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform == firstInteractor)
        {
            firstInteractor = null;
        }
        else
        {
            secondInteractor = null;
        }
    }

}