using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestBuffShow : MonoBehaviour
{
    Canvas buffShow;

    void Start()
    {
        buffShow = GameObject.Find("CanvasSelectBuff").GetComponent<Canvas>();
        buffShow.enabled = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha9)){
            buffShow.enabled = true;
        }  
        if (Input.GetKey(KeyCode.Alpha1)||Input.GetKey(KeyCode.Alpha2)||Input.GetKey(KeyCode.Alpha3)){
            buffShow.enabled = false;
        }                  
    }
}
