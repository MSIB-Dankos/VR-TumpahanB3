using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAPDHandler : MonoBehaviour
{
    public Animator uiAnimator;
    public CanvasGroup uiCanvasGroup;

    private int openCount;
    public void OpenUI()
    {
        if (openCount > 0)
        {
            return;
        }

        uiAnimator.SetTrigger("FadeIn");
        uiCanvasGroup.blocksRaycasts = true;
        openCount++;
    }
}
