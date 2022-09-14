using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public string sceneName;

    void Start()
    {
        Transition.instance.Fade(true, sceneName);
    }
}
