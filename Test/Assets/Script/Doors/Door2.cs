using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    public float speedX = 0;
    public float speedY = 0;
    public float speedZ = 0;

    void Start()
    {
        speedX = 0;
    }

    void Update()
    {
        transform.Translate(new Vector3(speedX, speedY, speedZ) * Time.deltaTime);
        if (Input.GetKey(KeyCode.P)){
            speedX = -15;
        }
        if(this.transform.position.x <= -20){
            speedX = 0;
        }

    }
}
