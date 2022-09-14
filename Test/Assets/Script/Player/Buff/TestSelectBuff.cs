using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSelectBuff : MonoBehaviour
{
    public GameObject TestSelectBuff1;
    public GameObject TestSelectBuff2;
    public GameObject TestSelectBuff3;

    void Start()
    {
        TestSelectBuff1.SetActive(true);
        TestSelectBuff2.SetActive(true);
        TestSelectBuff3.SetActive(true);

    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)){
            TestSelectBuff1.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha2)){
            TestSelectBuff2.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha3)){
            TestSelectBuff3.SetActive(false);
        }                   
    }
}
