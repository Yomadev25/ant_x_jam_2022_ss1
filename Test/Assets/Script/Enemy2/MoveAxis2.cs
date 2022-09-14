using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAxis2 : MonoBehaviour
{
    public float speedX = 0;
    public float speedY = 0;
    public float speedZ = 0;

    void Start()
    {
        speedZ = 8;
        this.GetComponent<Enemy2>().enabled = false;
    }

    void Update()
    {
        transform.Translate(new Vector3(speedX, speedY, speedZ) * Time.deltaTime);
        if (this.transform.position.z <= 20)
        {
            speedZ = 4;
        }
        if (this.transform.position.z <= 15)
        {
            speedZ = 0;
            this.GetComponent<Enemy2>().enabled = true;
        }
    }
}
