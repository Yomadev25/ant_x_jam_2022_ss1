using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGround : MonoBehaviour

{
    public GameObject respawnGround1 ;
//    public GameObject respawnGround2 ;
    float timeElapsed = 0;
    float ItemCycle = 3f;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > ItemCycle){
            GameObject temp;
            temp = (GameObject)Instantiate(respawnGround1);
            Vector3 pos = temp.transform.position;
            temp.transform.position = new Vector3(0,0,140f);
            timeElapsed -= ItemCycle;
        }
    }
}
