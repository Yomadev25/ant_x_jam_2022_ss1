using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    public GameObject bullet;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFire){
            nextFire = Time.time + fireRate;
            Fire();
        }
    }
    
    public void Fire()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
