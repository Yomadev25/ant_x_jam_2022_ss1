using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    Transform targetObject;
    public float fireRate = 1f;
    private float nextFire = 5.0f;
    public string tagObject;
    public GameObject enemyGun, enemyBullet4;

    void Start()
    {
        if (targetObject == null)
        {
            targetObject = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(enemyBullet4, enemyGun.transform.position, enemyGun.transform.rotation);
        }

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == tagObject)
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
