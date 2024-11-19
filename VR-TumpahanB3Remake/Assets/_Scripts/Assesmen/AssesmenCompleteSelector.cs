using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AssesmenCompleteSelector : MonoBehaviour
{
    public AssesmenController assesmenController;
    [ShowIf("assesmenController"), ValueDropdown("GetListAssesmen")] public string selectedAssesmen;

    public void CompleteAssesmen()
    {
        assesmenController.UpdateCompleteObjective(selectedAssesmen);
    }

#if UNITY_EDITOR
    private ValueDropdownList<string> GetListAssesmen()
    {
        if (!assesmenController)
        {
            return new ValueDropdownList<string>() { };
        }

        ValueDropdownList<string> assesmenList = new ValueDropdownList<string>();

        int index = 0;
        foreach (AssesmenController.Objective objective in assesmenController.objectives)
        {
            assesmenList.Add(string.Format($"{index} - {objective.objectiveName}"), objective.objectiveName);
            index++;
        }

        return assesmenList;
    }
#endif
}
