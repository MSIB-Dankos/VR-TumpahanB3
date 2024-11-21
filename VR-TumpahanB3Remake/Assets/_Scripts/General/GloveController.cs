using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class GloveController : MonoBehaviour, IXRSelectFilter
{
    [Header("Filter")]
    public List<XRBaseInteractor> allowedInteractor;
    public List<XRBaseInteractor> allowedInteractorWhenGloveMode;
    [field: SerializeField] public bool canProcess { get; set; }
    [field: SerializeField] public bool equipMode { get; set; }

    public bool assesmenMode;

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
    }

    private void OnSelect(SelectEnterEventArgs args)
    {
        if (!equipMode)
        {
            return;
        }

        if (gloveMode)
        {
            followTransform.enabled = false;
            handRenderer.material = originalMaterial;
            gloveRenderer.enabled = true;
            boxCollider.enabled = true;

            gloveMode = false;
            equipMode = false;
        }
        else
        {
            followTransform.enabled = true;
            handRenderer.material = gloveMaterial;
            gloveRenderer.enabled = false;
            boxCollider.enabled = false;

            gloveMode = true;
            equipMode = false;
        }

        if (assesmenMode)
        {
            SetEquipMode();
        }

    }

    public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
    {
        if (!(interactor is XRBaseInteractor inter))
        {
            return false;
        }

        if (inter.hasSelection)
        {
            return false;
        }

        if (gloveMode && allowedInteractorWhenGloveMode.Contains(inter))
        {
            if (Vector3.Distance(transform.position, inter.transform.position) < 0.1f)
            {
                return true;
            }
        }

        if (!gloveMode && allowedInteractor.Contains(inter))
        {
            return true;
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
