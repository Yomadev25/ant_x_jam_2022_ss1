using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float secondsUntilDestroy;
    float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        this.gameObject.transform.position += speed * this.gameObject.transform.forward;
        if (Time.time - startTime >= secondsUntilDestroy) Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
