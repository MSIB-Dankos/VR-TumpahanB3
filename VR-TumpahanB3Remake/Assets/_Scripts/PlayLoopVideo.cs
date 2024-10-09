using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(RawImage), typeof(VideoPlayer))]
public class PlayLoopVideo : MonoBehaviour
{
    public RenderTexture renderTemplate;

    private VideoPlayer videoPlayer;
    private RawImage rawImage;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        rawImage = GetComponent<RawImage>();

        rawImage.texture = new RenderTexture(renderTemplate);
        videoPlayer.targetTexture = rawImage.texture as RenderTexture;

        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
        videoPlayer.isLooping = true;
        videoPlayer.Play();
    }
}
