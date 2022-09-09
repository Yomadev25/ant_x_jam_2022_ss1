using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Transition.instance.Fade(true, sceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
