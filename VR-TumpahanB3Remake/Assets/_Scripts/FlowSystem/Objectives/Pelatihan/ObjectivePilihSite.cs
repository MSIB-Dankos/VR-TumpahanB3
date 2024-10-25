using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivePilihSite : FlowObjective
{
    [System.Serializable]
    public class SiteButton
    {
        public PKKTeamNumber.SITE site;
        public Button button;
    }

    public List<SiteButton> siteButtons = new List<SiteButton>();

    private bool hasSite = false;

    private void Awake()
    {
        foreach (SiteButton siteButton in siteButtons)
        {
            siteButton.button.onClick.AddListener(() =>
            {
                int siteID = (int)siteButton.site;
                PKKTeamNumber.SetSite(siteID);
                hasSite = true;
            });
        }
    }

    public override bool IsFlowComplete()
    {
        return hasSite;
    }
}
