using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PKKNumberUIText : MonoBehaviour
{
    public TMP_Text descriptionText;
    public string replaceWord = "{PKKNumber}";

    private void Awake()
    {
        string description = descriptionText.text;
        description = description.Replace(replaceWord, PKKTeamNumber.siteNumbers[PKKTeamNumber.currentSite]);
        descriptionText.text = description;
    }
}
