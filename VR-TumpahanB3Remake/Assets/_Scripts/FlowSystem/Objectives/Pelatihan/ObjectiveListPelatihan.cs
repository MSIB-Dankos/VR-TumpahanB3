using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EventBus))]
public class ObjectiveListPelatihan : FlowObjective
{
    [System.Serializable]
    public class PelatihanData
    {
        public string pelatihanName;
        public AudioClip voiceLinePelatihan;
        [ReadOnly, HideInEditorMode] public TMP_Text textUI;
    }

    public RectTransform textContainer;
    public TMP_Text prefabText;
    public AudioSource audioSource;
    public AudioClip firstAudio;
    public List<PelatihanData> pelatihanDataList;
    public string eventBeforeFlow = "OnStartFlow";

    private bool isDone = false;
    private EventBus eventBus;
    private void Awake()
    {
        eventBus = GetComponent<EventBus>();
        eventBus.AddListener(eventBeforeFlow, () => { StartCoroutine(UIandVoiceLineRoutine()); });
    }

    IEnumerator UIandVoiceLineRoutine()
    {
        audioSource.clip = firstAudio;
        audioSource.Play();
        yield return new WaitUntil(CheckVoiceLine);

        for (int i = 0; i < pelatihanDataList.Count; i++)
        {
            PelatihanData data = pelatihanDataList[i];

            data.textUI = Instantiate(prefabText, textContainer);
            data.textUI.text = data.pelatihanName;

            if (data.textUI.TryGetComponent(out Animator textAnim))
            {
                textAnim.SetTrigger("FadeIn");
            }

            if (data.voiceLinePelatihan)
            {
                audioSource.clip = pelatihanDataList[i].voiceLinePelatihan;
                audioSource.Play();
                yield return new WaitUntil(CheckVoiceLine);
            }
            yield return new WaitForSeconds(1.0f);
        }
        isDone = true;
    }

    private bool CheckVoiceLine()
    {
        return !audioSource.isPlaying;
    }

    public override bool IsFlowComplete()
    {
        return isDone;
    }
}
