using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public bool buff1;
    public bool buff2;
    public bool buff3;
    public GameObject gun2;
    public GameObject gun3;
    public GameObject buffBullet;
    
    void Start()
    {
        buff1 = false;
        buff2 = false;
        buff3 = false;
        this.GetComponent<Gun>().fireRate = 0.5f;
        gun2.SetActive(false);
        gun3.SetActive(false);
        buffBullet.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
    }
        void Update()
        {
                if (Input.GetKey(KeyCode.Alpha1)){
                        buff1 = true;
        }
                if (Input.GetKey(KeyCode.Alpha2)){
                        buff2 = true;
        }
                if (Input.GetKey(KeyCode.Alpha3)){
                        buff3 = true;
        }
                if(buff1 == true){
                        this.GetComponent<Gun>().fireRate = 0.2f;
        }
                if(buff2 == true){
                        gun2.SetActive(true);
                        gun3.SetActive(true);
        }
                if(buff3 == true){
                buffBullet.transform.localScale = new Vector3(1f,1f,1f);
        }
        }
}