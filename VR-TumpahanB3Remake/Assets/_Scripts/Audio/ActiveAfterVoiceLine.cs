using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActiveAfterVoiceLine : MonoBehaviour
{
    public List<GameObject> activateGameObjects = new List<GameObject>();
    public VoiceLineQueue voiceLineQueue;
    [ValueDropdown(nameof(GetTargetVoices))] public int targetVoiceIndex;

    private void Awake()
    {
        StartCoroutine(UpdateRoutine());
    }

    private IEnumerator UpdateRoutine()
    {
        WaitForSeconds timeUpdate = new WaitForSeconds(0.1f);
        while (true)
        {
            if (targetVoiceIndex != voiceLineQueue.GetCurrentAudioIndex())
            {
                yield return timeUpdate;
                continue;
            }

            if (voiceLineQueue.audioSource.isPlaying)
            {
                yield return timeUpdate;
                continue;
            }

            foreach (GameObject obj in activateGameObjects)
            {
                obj.SetActive(true);
            }
            yield break;
        }
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
