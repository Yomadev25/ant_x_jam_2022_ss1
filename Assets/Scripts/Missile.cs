using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _bomb;
    Rigidbody _rb;

    Vector3 _deviatedPrediction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _deviatedPrediction = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void Update()
    {
        RotateRocket();
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.forward * _speed;
    }

    private void RotateRocket()
    {
        var heading = _deviatedPrediction - transform.position;

        var rotation = Quaternion.LookRotation(heading);
        _rb.MoveRotation(rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Wall"))
        {
            Instantiate(_bomb, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
