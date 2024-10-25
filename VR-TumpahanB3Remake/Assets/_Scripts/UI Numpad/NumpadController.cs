using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;

[RequireComponent(typeof(TrackedDeviceGraphicRaycaster))]
public class NumpadController : MonoBehaviour
{
    public TMP_Text inputFieldText;
    public Button submitButton;
    public UnityEvent<string> OnSubmit;

    [ShowInInspector, ReadOnly] private string currentNumber;

    private TrackedDeviceGraphicRaycaster graphicRaycaster;

    private void Awake()
    {
        submitButton.onClick.AddListener(Submit);
        UpdateNumberText();

        graphicRaycaster = GetComponent<TrackedDeviceGraphicRaycaster>();
    }

    private void Submit()
    {
        OnSubmit?.Invoke(currentNumber);
        currentNumber = "";
        UpdateNumberText();
    }

    public void AddNumber(string number)
    {
        currentNumber += number;
        UpdateNumberText();
    }

    public void Backspace()
    {
        if (currentNumber.Length == 0)
        {
            return;
        }

        currentNumber = currentNumber.Substring(0, currentNumber.Length - 1);
        UpdateNumberText();
    }

    public void Clear()
    {
        currentNumber = "";
        UpdateNumberText();
    }

    public void Disable()
    {
        graphicRaycaster.enabled = false;
    }

    public void Enable()
    {
        graphicRaycaster.enabled = true;
    }

    private void UpdateNumberText()
    {
        inputFieldText.text = currentNumber;
    }
}
