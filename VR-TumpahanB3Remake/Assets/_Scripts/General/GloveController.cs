using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class GloveController : MonoBehaviour, IXRSelectFilter
{
    [Header("Filter")]
    public List<XRBaseInteractor> allowedInteractor;
    public Transform oppositeHand;
    public InputActionReference releaseGloveInput;
    public float distanceReleaseGlove = 0.05f;
    [field: SerializeField] public bool canProcess { get; set; }
    [field: SerializeField] public bool equipMode { get; set; }

    [Header("Hand Settings")]

    public FollowTransform followTransform;
    public Renderer handRenderer;
    public Material gloveMaterial, originalMaterial;
    public Renderer gloveRenderer;
    public BoxCollider boxCollider;

    private bool gloveMode;
    private XRBaseInteractable interactable;
    private void Awake()
    {
        interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(OnSelect);

        releaseGloveInput.action.started += OnOppisiteHandSelect;
    }

    private void OnOppisiteHandSelect(InputAction.CallbackContext ctx)
    {
        if (!equipMode)
        {
            return;
        }
        
        if (Vector3.Distance(oppositeHand.position, transform.position) < distanceReleaseGlove)
        {
            if (gloveMode)
            {
                followTransform.enabled = false;
                handRenderer.material = originalMaterial;
                gloveRenderer.enabled = true;
                boxCollider.enabled = true;

                gloveMode = false;
                equipMode = false;
            }
        }
    }

    private void OnSelect(SelectEnterEventArgs args)
    {
        if (!equipMode)
        {
            return;
        }

        IXRSelectInteractor interactor = args.interactorObject;

        if (!gloveMode)
        {
            followTransform.enabled = true;
            handRenderer.material = gloveMaterial;
            gloveRenderer.enabled = false;
            boxCollider.enabled = false;

            gloveMode = true;
            equipMode = false;
        }
    }

    public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
    {
        if (!gloveMode)
        {
            if (interactor is XRBaseInteractor inter)
            {
                if (allowedInteractor.Contains(inter))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void SetEquipMode()
    {
        equipMode = true;
        boxCollider.enabled = true;
    }

    public bool IsGloveMode()
    {
        return gloveMode;
    }
}