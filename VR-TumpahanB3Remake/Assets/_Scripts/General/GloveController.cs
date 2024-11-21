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

    [Header("Assesmen")]
    public bool assesmenMode;
    public bool isSocketFilled { get; set; }

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

            if (isSocketFilled)
            {
                return;
            }
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
        if (!(interactor is XRBaseInteractor _interactor))
        {
            return false;
        }

        if (_interactor.hasSelection)
        {
            return false;
        }

        if (gloveMode && allowedInteractorWhenGloveMode.Contains(_interactor))
        {
            if (Vector3.Distance(interactable.transform.position, _interactor.transform.position) < 0.05f)
            {
                return true;
            }
        }

        if (!gloveMode && allowedInteractor.Contains(_interactor))
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
