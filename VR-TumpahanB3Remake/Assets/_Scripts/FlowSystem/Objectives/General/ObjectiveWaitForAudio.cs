using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveWaitForAudio : FlowObjective
{
    public VoiceLineQueue voiceLineQueue;
    public int targetVoiceIndex;

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

        return true;
    }
}
