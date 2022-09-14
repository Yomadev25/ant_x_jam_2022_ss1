using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public static Transition instance;
    private CanvasGroup _panel;

    [Range(0f, 1f)]
    [SerializeField] private float _fadeSpeed;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        _panel = GetComponent<CanvasGroup>();
        _panel.alpha = 1f;

        Fade(false);
    }

    public void Fade(bool isFadeIn, string sceneName = null)
    {
        StartCoroutine(FadeAnimation(isFadeIn, sceneName));
    }

    IEnumerator FadeAnimation(bool isFadeIn, string sceneName = null)
    {
        if (isFadeIn)
        {
            _panel.LeanAlpha(1, _fadeSpeed);
            yield return new WaitForSeconds(_fadeSpeed);
            if (sceneName != null) SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else
        {
            _panel.LeanAlpha(0, _fadeSpeed).setDelay(1f);
        }
    }

}
