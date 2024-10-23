using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CleanFluid : MonoBehaviour
{
    public float cleanTime;
    public GameObject cleanerObject;

    private float currentTime;
    private Animator animator;
    private bool isClean = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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
