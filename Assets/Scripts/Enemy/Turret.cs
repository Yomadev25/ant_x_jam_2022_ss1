using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public enum AIState { Idle, Attack }
    private AIState currentState;

    [SerializeField] private float _hp;

    [Header("AI REFERENCES")]
    [SerializeField] private float _maxDistance;
    [SerializeField] private Transform _headPos;
    private Transform _targetPos;

    [Header("GUN REFERENCES")]
    [SerializeField] private GameObject _bulletPrefabs;
    [SerializeField] private Transform _gunPos;
    [SerializeField] private int _ammo;

    private BoxCollider _collider;
    bool isDie;

    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _targetPos = GameObject.FindGameObjectWithTag("Player").transform;
        if (_targetPos == null) this.enabled = false;

        StateChange(AIState.Idle);
    }

    void Update()
    {
        if (_hp <= 0)
        {
            if (isDie) return;
            isDie = true;
            StartCoroutine(Die());
        }
        else
        {
            Idle();
        }
    }

    #region AI STATE
    private void StateChange(AIState state)
    {
        switch (state)
        {
            case AIState.Idle:
                Idle();
                break;
            case AIState.Attack:
                StartCoroutine(Attack());
                break;
        }
        currentState = state;
    }

    private void Idle() //FIND PLAYER
    {
        if (currentState != AIState.Idle) return;

        if (Vector3.Magnitude((this.transform.position - _targetPos.transform.position)) <= _maxDistance)
        {
            StateChange(AIState.Attack);
        }
    }

    IEnumerator Attack() //ATTACK PLAYER
    {
        if (currentState != AIState.Attack) yield return null;
       
        float t = 0;
        while (t < 1.5f)
        {
            Vector3 direction = (_targetPos.position - _headPos.position).normalized;
            Quaternion rotGoal = Quaternion.LookRotation(direction);
            _headPos.rotation = Quaternion.Slerp(_headPos.rotation, rotGoal, 5f);
            yield return new WaitForSeconds(0.01f);
            t += 0.01f;
        }

        for (int i = 0; i < _ammo; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(_bulletPrefabs, _gunPos.position, _gunPos.rotation);
        }

        yield return new WaitForSeconds(1f);
        StateChange(AIState.Idle);
    }
    #endregion

    IEnumerator Die()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        //Boom Effect;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            _hp--;
        }
    }
}