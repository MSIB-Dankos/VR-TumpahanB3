using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class TelephoneAssesmen : MonoBehaviour
{
    public NumpadController numpadController;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Events")]
    public UnityEvent onFailed;
    public UnityEvent onCorrect;

    [ShowInInspector, ReadOnly] private string targetNumber => PKKTeamNumber.siteNumbers[PKKTeamNumber.currentSite];

    private void OnEnable()
    {
        numpadController.OnSubmit.AddListener(OnCall);
    }

    private void OnDisable()
    {
        numpadController.OnSubmit.RemoveListener(OnCall);
    }

    private void OnCall(string number)
    {
        StartCoroutine(CallTextAnimateRoutine());

        IEnumerator CallTextAnimateRoutine()
        {
            numpadController.Clear();
            numpadController.Disable();

            WaitForSeconds textDotWait = new WaitForSeconds(0.25f);

            for (int i = 0; i < 16; i++)
            {
                if (!audioSource.isPlaying && i % 4 == 0) audioSource.Play();
                numpadController.AddNumber(".");
                if (i % 3 == 0)
                {
                    numpadController.Clear();
                }

                if (i % 8 == 0) audioSource.Stop();
                yield return textDotWait;
            }
            numpadController.Clear();

            bool isCorrect = targetNumber == number;
            if (isCorrect)
            {
                onCorrect?.Invoke();
            }
            else
            {
                onFailed?.Invoke();
            }


            numpadController.Enable();
            audioSource.Stop();
        }
    }
}
