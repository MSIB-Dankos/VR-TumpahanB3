using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketInteractorGlove : SocketInteractorAllowedObject //lmaooo
{
    public List<GloveController> gloveControllers;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        if (interactable is GloveController grabInteractable)
        {
            if (gloveControllers.Contains(grabInteractable))
            {
                if (!grabInteractable.IsGloveMode())
                {
                    return base.BaseCanHover(interactable);
                }
            }
        }
        return false;
    }

    protected override bool CanHoverSnap(IXRInteractable interactable)
    {
        if (interactable is GloveController grabInteractable)
        {
            if (gloveControllers.Contains(grabInteractable))
            {
                if (!grabInteractable.IsGloveMode())
                {
                    return base.BaseCanHoverSnap(interactable);
                }
            }
        }
        return false;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        if (interactable is GloveController grabInteractable)
        {
            if (gloveControllers.Contains(grabInteractable))
            {
                if (!grabInteractable.IsGloveMode())
                {
                    return base.BaseCanSelect(interactable);
                }
            }
        }
        return false;
    }
}
