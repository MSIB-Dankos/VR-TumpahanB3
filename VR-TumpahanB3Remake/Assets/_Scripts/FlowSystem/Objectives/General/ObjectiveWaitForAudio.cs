using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ObjectiveWaitForAudio : FlowObjective
{
    public VoiceLineQueue voiceLineQueue;
    [ValueDropdown(nameof(GetTargetVoices))] public int targetVoiceIndex;

    [Header("Filter")]
    public List<FlowFilter> filters = new List<FlowFilter>();

    public override bool IsFlowComplete()
    {
        if (targetVoiceIndex != voiceLineQueue.GetCurrentAudioIndex())
        {
            return false;
        }

        if (voiceLineQueue.audioSource.isPlaying)
        {
            return false;
        }

        if (filters.Count > 1)
        {
            foreach (FlowFilter flowFilter in filters)
            {
                if (!flowFilter.GetFilter())
                {
                    return false;
                }
            }
        }
        else if (filters.Count > 0)
        {
            if (!filters[0].GetFilter())
            {
                return false;
            }
        }

        return true;
    }

    private ValueDropdownList<int> GetTargetVoices()
    {
#if UNITY_EDITOR
        if (!voiceLineQueue)
        {
            return null;
        }
        ValueDropdownList<int> listVoice = new ValueDropdownList<int>();
        for (int i = 0; i < voiceLineQueue.voiceQueue.Count; i++)
        {
            listVoice.Add(voiceLineQueue.voiceQueue[i].audioClip.name + " - " + i, i);
        }
        return listVoice;
#else
        return null;
#endif
    }
}
