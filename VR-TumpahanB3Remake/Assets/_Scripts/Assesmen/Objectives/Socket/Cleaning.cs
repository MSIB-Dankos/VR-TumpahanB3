using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cleaning : MonoBehaviour
{
    public List<CleanFluid> cleanFluids;

    public UnityEvent onFluidClean;
    private void Awake()
    {
        StartCoroutine(UpdateCleaning());
    }

    public IEnumerator UpdateCleaning()
    {
        WaitForSeconds updateTime = new WaitForSeconds(0.1f);
        while (true)
        {
            bool allClean = true;

            foreach (CleanFluid cleanFluid in cleanFluids)
            {
                if (!cleanFluid.IsClean())
                {
                    allClean = false; // Not all are clean, set the flag to false
                    break; // Exit the loop as one fluid is not clean
                }
            }

            if (allClean)
            {
                onFluidClean?.Invoke(); // Invoke the event only when all are clean
                yield break; // Exit the coroutine
            }

            yield return updateTime; // Wait before re-checking
        }
    }

}
