using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketInteractorAllowedObject : XRSocketInteractor
{
    public List<XRGrabInteractable> allowedInteractables;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        if (interactable is XRGrabInteractable grabInteractable)
        {
            if (allowedInteractables.Contains(grabInteractable))
            {
                return base.CanHover(interactable);
            }
        }
        return false;
    }

    protected override bool CanHoverSnap(IXRInteractable interactable)
    {
        if (interactable is XRGrabInteractable grabInteractable)
        {
            if (allowedInteractables.Contains(grabInteractable))
            {
                return base.CanHoverSnap(interactable);
            }
        }
        return false;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        if (interactable is XRGrabInteractable grabInteractable)
        {
            if (allowedInteractables.Contains(grabInteractable))
            {
                return base.CanSelect(interactable);
            }
        }
        return false;
    }
}