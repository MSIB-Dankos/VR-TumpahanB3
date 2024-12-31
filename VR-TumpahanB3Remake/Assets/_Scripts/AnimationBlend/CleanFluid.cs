using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanFluid : MonoBehaviour
{
    public float cleanTime;
    public GameObject cleanerObject;
    public MeshRendererController meshRendererController;
    public Animator animator;

    public float darkerAmount = 1.0f;

    private float currentTime;
    private bool isClean = false;

    public bool IsClean()
    {
        return isClean;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == cleanerObject)
        {
            Debug.Log("is Clean");
            Color currentColor = meshRendererController.meshRenderers[0].material.color;
            meshRendererController.SetColors(new Color(
                currentColor.r - darkerAmount,
                currentColor.g - darkerAmount,
                currentColor.b - darkerAmount
            ));
            animator.SetFloat("Clean", 1.0f);
            isClean = true;
        }
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.gameObject == cleanerObject)
    //     {
    //         currentTime += Time.fixedDeltaTime;
    //         currentTime = Mathf.Clamp(currentTime, 0, cleanTime);
    //         float value = currentTime / cleanTime;

    //         animator.SetFloat("Clean", value);

    //         value = darkerAmount * Time.fixedDeltaTime;
    //         Color currentColor = meshRendererController.meshRenderers[0].material.color;
    //         meshRendererController.SetColors(new Color(
    //             currentColor.r - value,
    //             currentColor.g - value,
    //             currentColor.b - value
    //         ));

    //         if (currentTime == cleanTime)
    //         {
    //             isClean = true;
    //         }
    //     }
    // }
}
