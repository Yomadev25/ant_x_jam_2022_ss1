using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float _hp;
    public float _maxHp;

    void Start()
    {
        _maxHp = _hp;
    }

    void Update()
    {
        
    }

    public void Heal(float heal)
    {
        _hp += heal;
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;
    }

    void Die()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            TakeDamage(1f);
        }
    }
}
