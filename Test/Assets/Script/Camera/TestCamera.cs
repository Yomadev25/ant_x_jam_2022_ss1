using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    public Transform player;
    public float speed = 5;

    void Update()
    {
        transform.RotateAround(player.position,Vector3.up,speed*Time.deltaTime);
    }
}
