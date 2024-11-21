using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketActiveWhenSelect : SocketInteractorAllowedObject
{
    public XRGrabInteractable interactable;
    public GameObject hoverObj;

    private bool canAttach;
    private Coroutine requestFalseAttach;

    protected override void Awake()
    {
        base.Awake();

        interactable.selectEntered.AddListener(OnSelect);
        interactable.selectExited.AddListener(OnExit);

        hoverObj.SetActive(false);
    }

    private void OnSelect(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor directInteractor)
        {
            if (requestFalseAttach != null)
            {
                StopCoroutine(requestFalseAttach);
                requestFalseAttach = null;
            }
            canAttach = true;
            hoverObj.SetActive(true);
        }
    }

    private void OnExit(SelectExitEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor directInteractor)
        {
            requestFalseAttach = StartCoroutine(RequestFalseAttachRoutine());
            hoverObj.SetActive(false);
        }
    }

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        if (requestFalseAttach != null)
        {
            StopCoroutine(requestFalseAttach);
            requestFalseAttach = null;
        }
        return base.CanHover(interactable) && canAttach;
    }

    protected override bool CanHoverSnap(IXRInteractable interactable)
    {
        if (requestFalseAttach != null)
        {
            StopCoroutine(requestFalseAttach);
            requestFalseAttach = null;
        }
        return base.CanHoverSnap(interactable) && canAttach;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        if (requestFalseAttach != null)
        {
            StopCoroutine(requestFalseAttach);
            requestFalseAttach = null;
        }
        return base.CanSelect(interactable) && canAttach;
    }

    private IEnumerator RequestFalseAttachRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        canAttach = false;
    }
}
