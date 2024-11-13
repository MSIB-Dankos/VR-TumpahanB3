using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketInteractorGlove : SocketInteractorAllowedObject //lmaooo
{
    public List<GloveController> gloveControllers;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        if (interactable.transform.TryGetComponent(out GloveController gloveController))
        {
            if (gloveControllers.Contains(gloveController))
            {
                if (!gloveController.IsGloveMode())
                {
                    return base.BaseCanHover(interactable);
                }
            }
        }
        return false;
    }

    protected override bool CanHoverSnap(IXRInteractable interactable)
    {
        if (interactable.transform.TryGetComponent(out GloveController gloveController))
        {
            if (gloveControllers.Contains(gloveController))
            {
                if (!gloveController.IsGloveMode())
                {
                    return base.BaseCanHoverSnap(interactable);
                }
            }
        }
        return false;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        if (interactable.transform.TryGetComponent(out GloveController gloveController))
        {
            if (gloveControllers.Contains(gloveController))
            {
                if (!gloveController.IsGloveMode())
                {
                    return base.BaseCanSelect(interactable);
                }
            }
        }
        return false;
    }
}
