using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanFluid : MonoBehaviour
{
    public float cleanTime;
    public GameObject cleanerObject;
    public Animator animator;

    private float currentTime;
    private bool isClean = false;

    public bool IsClean()
    {
        return isClean;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == cleanerObject)
        {
            currentTime += Time.fixedDeltaTime;
            currentTime = Mathf.Clamp(currentTime, 0, cleanTime);
            animator.SetFloat("Clean", currentTime / cleanTime);

            if (currentTime == cleanTime)
            {
                isClean = true;
            }
        }
    }
}
