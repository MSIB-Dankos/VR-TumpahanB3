using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class GloveController : MonoBehaviour, IXRSelectFilter
{
    public FollowTransform followTransform;
    public XRGrabInteractable XRGrab;
    public Renderer leftGlove, rightGlove;
    public Renderer leftHand, rightHand;
    public Material gloveMaterial, defaultMaterial;
    public Transform centerHandFollow;
    [field: SerializeField] public bool canProcess { get; set; }

    [Header("Debug")]
    [ShowInInspector, ReadOnly] private bool gloveMode;

    private void Awake()
    {
        followTransform.target = null;
        XRGrab.selectEntered.AddListener(OnSelect);
    }

    private void OnSelect(SelectEnterEventArgs args)
    {
        if (args.interactorObject is SocketInteractorAllowedObject || args.interactableObject is XRSocketInteractor)
        {
            return;
        }
        if (gloveMode)
        {
            leftGlove.enabled = true;
            rightGlove.enabled = true;

            rightHand.material = defaultMaterial;
            leftHand.material = defaultMaterial;

            followTransform.target = null;

            gloveMode = false;
        }
        else
        {
            leftGlove.enabled = false;
            rightGlove.enabled = false;

            rightHand.material = gloveMaterial;
            leftHand.material = gloveMaterial;

            followTransform.target = centerHandFollow;

            gloveMode = true;
        }
    }

    public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
    {
        return canProcess;
    }
}
