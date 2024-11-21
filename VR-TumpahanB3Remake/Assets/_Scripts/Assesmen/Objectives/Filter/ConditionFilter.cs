using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class ConditionFilter : MonoBehaviour, IXRSelectFilter
{
    [field: SerializeField] public bool allowed { get; set; }

    bool IXRSelectFilter.canProcess => false;

    bool IXRSelectFilter.Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
    {
        return allowed;
    }
}
