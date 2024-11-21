using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeObjectiveToUI : MonoBehaviour
{
    public GameObject targetObj;
    public RaycastObject raycastObject;

    [Header("UI")]
    public CanvasGroup canvasGroup;
    public Animator canvasAnimator;

    private void Awake()
    {
        StartCoroutine(UpdateCheck());
    }

    private IEnumerator UpdateCheck()
    {
        bool find = false;
        WaitForSeconds updateTime = new WaitForSeconds(0.2f);

        while (!find)
        {
            raycastObject.CheckRaycast(() =>
            {
                if (raycastObject.GetRaycastHit().collider.gameObject == targetObj)
                {
                    find = true;
                }
            });
            yield return updateTime;
        }

        canvasAnimator.SetTrigger("FadeIn");
        canvasGroup.blocksRaycasts = true;
    }
}
