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
            foreach (CleanFluid cleanFluid in cleanFluids)
            {
                if (!cleanFluid.IsClean())
                {
                    yield return updateTime;
                    continue;
                }
            }
            
            yield return updateTime;
        }
    }
}
