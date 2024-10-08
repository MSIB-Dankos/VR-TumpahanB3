using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NumpadController : MonoBehaviour
{
    public TMP_Text inputFieldText;
    public Button submitButton;
    public UnityEvent OnSubmit;

    [ShowInInspector, ReadOnly] private string currentNumber;

    private void Awake()
    {
        submitButton.onClick.AddListener(Submit);
        UpdateNumberText();
    }

    private void Submit()
    {
        OnSubmit?.Invoke();
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
        currentNumber = currentNumber.Substring(0, currentNumber.Length - 1);
        UpdateNumberText();
    }

    private void UpdateNumberText()
    {
        inputFieldText.text = currentNumber;
    }
}
