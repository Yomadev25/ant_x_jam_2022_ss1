using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float secondsUntilDestroy;
    [SerializeField] private GameObject _effect;
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
        if (this.tag != "BossBullet")
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy") || other.CompareTag("Wall"))
            {
                Instantiate(_effect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }
}
