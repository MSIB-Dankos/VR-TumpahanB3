using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class SelectGloveFilter : MonoBehaviour, IXRSelectFilter
{
    public List<GloveController> gloveControllers;
    [field: SerializeField] public bool canProcess { get; set; }

    public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
    {
        if (interactable.transform.TryGetComponent(out GloveController gloveController))
        {
            if (gloveControllers.Contains(gloveController))
            {
                if (!gloveController.IsGloveMode())
                {
                    return true;
                }
            }
        }
        return false;
    }
}
