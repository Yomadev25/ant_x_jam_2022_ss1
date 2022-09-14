using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAxis : MonoBehaviour
{
    public float speedX = 0;
    public float speedY = 0;
    public float speedZ = 0;
    public float gravity = 5f;

    void Start()
    {
        speedZ = 8;
        this.GetComponent<Enemy1>().enabled = false;
    }

    void Update()
    {
        transform.Translate(new Vector3(speedX, speedY, speedZ) * Time.deltaTime);
        if(this.transform.position.z <= 20){
            speedZ = 4;
        }
        if(this.transform.position.z <= 15){
            speedZ = 0;
            this.GetComponent<Enemy1>().enabled = true;
        }

    }
}
