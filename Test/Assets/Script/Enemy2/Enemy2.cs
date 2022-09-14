using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    Transform targetObject;
    public float fireRate = 1f;
    private float nextFire = 5.0f;
    public string tagObject;
    public GameObject enemyGun, enemyBullet2;

    void Start()
    {
        if (targetObject == null)
        {
            targetObject = GameObject.FindGameObjectWithTag("Player").transform;
        }
        StartCoroutine(Shoot());
    }

    void Update()
    {
        /*if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(enemyBullet1, enemyGun.transform.position, enemyGun.transform.rotation);
        }*/
        

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == tagObject)
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
    IEnumerator Shoot()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(fireRate);
            Instantiate(enemyBullet2, enemyGun.transform.position, enemyGun.transform.rotation);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(Shoot());
    }
}
