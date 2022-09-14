using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float speed = 0.5f;
    public float objectSpeed = 0.01f;
    void Start()
    {
        Time.timeScale = 1;

    }

    void Update()
    {
        float offset = Time.time * speed;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, -offset);
        transform.Translate(0, 0, -(objectSpeed)); 
                
    }
}
