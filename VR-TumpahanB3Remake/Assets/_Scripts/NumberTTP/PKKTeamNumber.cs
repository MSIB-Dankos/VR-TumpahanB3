using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[InfoBox("SITES ID:\n - KF \t= 0\n - DF \t= 1\n - GOF \t= 2\n - FIMA \t= 3\n - HJ \t= 4")]
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
    public static PKKTeamNumber pkk;
    private void Awake()
    {
        if (pkk)
        {
            Destroy(pkk.gameObject);
        }
        pkk = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void SetSite(int siteID)
    {
        currentSite = (SITE)siteID;
        Debug.Log($"Set SITE = {currentSite} : {siteNumbers[currentSite]}");
    }
}
