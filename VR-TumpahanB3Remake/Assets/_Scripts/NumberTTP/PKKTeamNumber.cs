using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKKTeamNumber : MonoBehaviour
{
    public enum SITE
    {
        KF,
        DF,
        GOF,
        FIMA,
        HJ
    }

    public static readonly Dictionary<SITE, string> siteNumbers = new Dictionary<SITE, string>
    {
        {SITE.KF, "111"},
        {SITE.DF, "1509"},
        {SITE.GOF, "1507"},
        {SITE.FIMA, "9000"},
        {SITE.HJ, "1105"}
    };

    public static SITE currentSite { get; private set; } = SITE.KF;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void SetSite(int siteID)
    {
        currentSite = (SITE)siteID;
        Debug.Log($"Set SITE = {currentSite} : {siteNumbers[currentSite]}");
    }
}
