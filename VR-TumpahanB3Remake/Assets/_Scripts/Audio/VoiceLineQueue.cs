using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class VoiceLineQueue : MonoBehaviour
{
    [System.Serializable]
    public class AudioData
    {
        public AudioClip audioClip;
        public bool canSkipped;
    }

    [TableList(ShowIndexLabels = true)] public List<AudioData> voiceQueue = new List<AudioData>();
    public AudioSource audioSource;

    private int currentAudioIndex = 0;
    private int queueCount;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.loop = false;
        audioSource.clip = voiceQueue[currentAudioIndex].audioClip;
        audioSource.Play();
    }

    private void Update()
    {
        if (queueCount > 0)
        {
            if (!audioSource.isPlaying)
            {
                currentAudioIndex = Mathf.Clamp(currentAudioIndex + queueCount, 0, voiceQueue.Count - 1);
                audioSource.clip = voiceQueue[currentAudioIndex].audioClip;
                audioSource.Play();
                queueCount = 0;
            }
        }
    }

    public void RestartAudio()
    {
        audioSource.clip = voiceQueue[currentAudioIndex].audioClip;
        audioSource.Play();
    }

    public void NextAudio()
    {
        if (voiceQueue[currentAudioIndex].canSkipped)
        {
            audioSource.Stop();
            currentAudioIndex = Mathf.Clamp(currentAudioIndex + 1, 0, voiceQueue.Count - 1);
            audioSource.clip = voiceQueue[currentAudioIndex].audioClip;
            audioSource.Play();
        }
        else
        {
            queueCount++;
        }
    }

    public int GetCurrentAudioIndex()
    {
        return currentAudioIndex;
    }
}
