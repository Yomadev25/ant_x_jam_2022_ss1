using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public enum AIState { Idle, Patrol, Attack }
    private AIState currentState;

    [SerializeField] private float _hp;
    private float _maxHp;
    [SerializeField] private Image _hpBar;

    [Header("AI REFERENCES")]
    [SerializeField] private float _maxDistance;
    [SerializeField] private Animator _anim;
    private Transform _targetPos;
    private NavMeshAgent _navMesh;

    [Header("GUN REFERENCES")]
    [SerializeField] private GameObject _bulletPrefabs;
    [SerializeField] private Transform[] _gunPos;
    [SerializeField] private int _ammo;

    [Header("EFFECTS")]
    [SerializeField] private GameObject _dieEffect;

    bool isAttack;
    bool isDie;

    void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _targetPos = GameObject.FindGameObjectWithTag("Player").transform;
        if (_targetPos == null) this.enabled = false;
        _maxHp = _hp;

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
            Patrol();
        }

        _hpBar.fillAmount = _hp / _maxHp;
    }

    #region AI STATE
    private void StateChange(AIState state)
    {
        switch (state)
        {
            case AIState.Idle:
                Idle();
                break;
            case AIState.Patrol:
                Patrol();
                break;
            case AIState.Attack:
                StartCoroutine(Attack());
                break;
        }
        currentState = state;       
    }

    private void Idle() //CHECK PLAYER
    {       
        if (currentState != AIState.Idle) return;
        Debug.Log("BOSS IS IDLE");
        _navMesh.isStopped = true;
        _anim.SetBool("isPatrol", false);

        if (Vector3.Magnitude((this.transform.position - _targetPos.transform.position)) <= _maxDistance)
        {
            StateChange(AIState.Patrol);
        }
    }

    private void Patrol() //CHASE PLAYER
    {      
        if (currentState != AIState.Patrol) return;
        Debug.Log("BOSS IS PATROL");
        _navMesh.isStopped = false;
        _navMesh.destination = _targetPos.position;
        _anim.SetBool("isPatrol", true);

        if (_navMesh.remainingDistance <= _navMesh.stoppingDistance)
        {
            StateChange(AIState.Attack);
        }
        if (_navMesh.remainingDistance >= _maxDistance)
        {
            StateChange(AIState.Idle);
        }
    }

    IEnumerator Attack() //ATTACK PLAYER
    {      
        if (currentState != AIState.Attack) yield return null;
        Debug.Log("BOSS IS ATTACK");

        int index = 0; //Random
        switch (index)
        {
            case 0:
                NormalShoot();
                break;
            /*case 1:
                Missile();
                break;
            case 2:
                Stomp();
                break;
            case 3:
                Dash();
                break;*/
        }
    }

    IEnumerator NormalShoot()
    {
        if (isAttack) yield return null;

        Debug.Log("BOSS IS ATTACK BY NORMAL SHOOT");
        isAttack = true;
        _navMesh.isStopped = true;       
        this.transform.LookAt(_targetPos.position);
        _gunPos[0].LookAt(_targetPos.position);
        _gunPos[1].LookAt(_targetPos.position);
        _anim.SetBool("isPatrol", false);

        for (int i = 0; i < _ammo; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(_bulletPrefabs, _gunPos[0].position, _gunPos[0].rotation);
            Instantiate(_bulletPrefabs, _gunPos[1].position, _gunPos[1].rotation);
        }

        yield return new WaitForSeconds(1f);
        isAttack = false;
        StateChange(AIState.Patrol);
    }

    void Missile()
    {
        
    }

    void Stomp()
    {

    }

    void Dash()
    {

    }

    void Rest()
    {

    }
    #endregion

    IEnumerator Die()
    {
        Instantiate(_dieEffect, transform.position, transform.rotation);
        _anim.SetTrigger("Death");
        _navMesh.isStopped = true;
        yield return new WaitForSeconds(1.5f);       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            _hp--;
        }
    }
}
