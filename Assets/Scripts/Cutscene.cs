using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private VideoPlayer _video;

    private void Start()
    {
        _video.loopPointReached += ChangeScene;
    }

    public void ChangeScene(VideoPlayer vp)
    {
        Transition.instance.Fade(true, "Level1");
    }
}
