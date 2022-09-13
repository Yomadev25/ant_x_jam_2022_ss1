using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private VideoPlayer _video;
    double duration;

    private void Start()
    {
        duration = _video.clip.length;
    }

    private void Update()
    {
        double currentTime = _video.time;
        if (currentTime >= duration)
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        Transition.instance.Fade(true, "Level1");
    }
}
