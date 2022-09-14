using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestClickBuff : MonoBehaviour
{
    public GameObject TestBuff1;
    public GameObject TestBuff2;
    public GameObject TestBuff3;

    void Start()
    {
        TestBuff1.SetActive(false);
        TestBuff2.SetActive(false);
        TestBuff3.SetActive(false);

    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)){
            TestBuff1.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Alpha2)){
            TestBuff2.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Alpha3)){
            TestBuff3.SetActive(true);
        }                       
    }
}
