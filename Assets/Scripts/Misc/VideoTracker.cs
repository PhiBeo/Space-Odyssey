using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Video;

public class VideoTracker : MonoBehaviour
{
    [SerializeField] SceneType sceneType;
    VideoPlayer player;
    double videoLength;
    float timeTracker = 0;

    void Start()
    {
        player = GetComponent<VideoPlayer>();
        videoLength = player.length;
        Debug.Log(videoLength);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeTracker > videoLength)
        {
            if (sceneType == SceneType.Intro)
            {
                GameManager.instance.FinishClip(true);
            }
            else if (sceneType == SceneType.Outro)
            {
                GameManager.instance.FinishClip(false);
            }
        }

        timeTracker += Time.deltaTime;
    }
}
