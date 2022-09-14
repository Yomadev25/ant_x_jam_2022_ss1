using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet3 : MonoBehaviour
{
    public float Speed = 0.7f;
    public float SecondsUntilDestroy = 3.0f;
    float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        this.gameObject.transform.position += Speed * this.gameObject.transform.forward;
        if (Time.time - startTime >= SecondsUntilDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
