using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class VoiceLineWhenDistance : MonoBehaviour
{
    [Header("Settings")]
    [ReadOnly] public bool activeCheck;
    public Transform target;
    public Transform player;
    public float updateTime;
    public float distanceThreshold = 3.0f;

    [Header("Audio")]
    public AudioSource audioSource3D;
    public AudioClip clip;

    private WaitForSeconds wait;
    private void Awake()
    {
        StartCoroutine(CheckDistance());
    }

    private IEnumerator CheckDistance()
    {
        wait = new WaitForSeconds(updateTime);

        while (true)
        {
#if UNITY_EDITOR
            wait = new WaitForSeconds(updateTime);
#endif
            yield return wait;
            if (activeCheck)
            {

                if ((target.position - player.position).sqrMagnitude > distanceThreshold)
                {
                    if (!audioSource3D.isPlaying)
                    {
                        audioSource3D.clip = clip;
                        audioSource3D.Play();
                    }
                }
            }
        }
    }

    public void SetActiveCheck(bool active)
    {
        activeCheck = active;
    }
}
