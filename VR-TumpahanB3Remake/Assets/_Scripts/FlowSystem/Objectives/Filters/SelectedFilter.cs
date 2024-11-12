using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectedFilter : FlowFilter
{
    public List<XRBaseInteractable> interactableSelection = new List<XRBaseInteractable>();

    private HashSet<XRBaseInteractable> interactableSelected = new HashSet<XRBaseInteractable>();
    private void Awake()
    {
        foreach (XRBaseInteractable interactableSelected in interactableSelection)
        {
            interactableSelected.selectEntered.AddListener(OnSelectEnter);
        }
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        if (!(args.interactableObject is XRBaseInteractable interactable))
        {
            return;
        }

        interactableSelected.Add(interactable);
    }

    public override bool GetFilter()
    {
        return interactableSelected.Count == interactableSelection.Count;
    }
}
