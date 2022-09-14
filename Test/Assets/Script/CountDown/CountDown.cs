using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public float timeLeft = 10f;
    Text showTime;
    Canvas gameOver;

    void Start()
    {
        showTime = GameObject.Find("/Canvas/TextShowTime").GetComponent<Text>();
        gameOver = GameObject.Find("/Canvas/GameOverManu").GetComponent<Canvas>();
        gameOver.enabled = false;

    }

    void Update()
    {
        if (timeLeft > 0){
            timeLeft -= Time.deltaTime;
            showTime.text = timeLeft.ToString("###0");
        }
        else{
            gameOver.enabled = true;
        }
    }
}
