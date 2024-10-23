using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCleanFluid : FlowObjective
{
    public List<CleanFluid> cleanFluids;

    public override bool IsFlowComplete()
    {
        foreach (CleanFluid cleanFluid in cleanFluids)
        {
            if (!cleanFluid.IsClean())
            {
                return false;
            }
        }

        return true;
    }
}
