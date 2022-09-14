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
    [SerializeField] private GameObject _barrier;
    private Transform _targetPos;
    private NavMeshAgent _navMesh;

    [Header("GUN REFERENCES")]
    [SerializeField] private GameObject _bulletPrefabs;
    [SerializeField] private GameObject _missilePrefabs;
    [SerializeField] private GameObject _lineOfSight;
    [SerializeField] private Transform[] _gunPos;
    [SerializeField] private int _ammo;

    [Header("EFFECTS")]
    [SerializeField] private GameObject _dieEffect;
    [SerializeField] private AudioSource _boomSound;

    int damage = 0;
    bool isPhaseTwo = false;
    bool isAttack;
    bool isRest;
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
            if(!isRest) Patrol();
        }

        if (_hp <= 350)
        {
            if(!isPhaseTwo) PhaseTwo();
        }

        if (damage >= 100)
            Rest();

        _barrier.SetActive(isRest);
        _hpBar.fillAmount = _hp / _maxHp;
    }

    public void PhaseTwo()
    {
        Debug.Log("PHASE TWO");
        isPhaseTwo = true;
        _ammo += 3;
        _navMesh.speed += 5;
        _anim.SetBool("isPhaseTwo", true);
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

        if (Vector3.Magnitude((this.transform.position - _targetPos.transform.position)) <= _maxDistance && !isRest)
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
        if (_navMesh.remainingDistance >= _maxDistance || isRest)
        {
            StateChange(AIState.Idle);
        }
    }

    IEnumerator Attack() //ATTACK PLAYER
    {      
        if (currentState != AIState.Attack) yield return null;
        Debug.Log("BOSS IS ATTACK");

        int index = Random.Range(0, 2); //Random
        switch (index)
        {
            case 0:
                StartCoroutine(NormalShoot());
                break;
            case 1:
                StartCoroutine(Missile());
                break;
            /*case 2:
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

    IEnumerator Missile()
    {
        if (isAttack) yield return null;

        Debug.Log("BOSS IS ATTACK BY MISSILE");
        isAttack = true;

        _navMesh.isStopped = true;
        this.transform.LookAt(_targetPos.position);
        _lineOfSight.SetActive(true);
        _anim.SetBool("isPatrol", false);

        float duration = 3f;
        while (duration > 0)
        {
            _gunPos[0].LookAt(_targetPos.position);
            _gunPos[1].LookAt(_targetPos.position);
            duration -= Time.deltaTime;
            yield return null;
        }
        _lineOfSight.SetActive(false);

        Instantiate(_missilePrefabs, _gunPos[0].transform.position, _gunPos[0].transform.rotation);
        if (isPhaseTwo)
        {
            yield return new WaitForSeconds(0.1f);
            Instantiate(_missilePrefabs, _gunPos[1].transform.position, _gunPos[1].transform.rotation);
        }

        yield return new WaitForSeconds(1f);
        isAttack = false;
        StateChange(AIState.Patrol);
    }

    IEnumerator Stomp()
    {
        if (isAttack) yield return null;

        Debug.Log("BOSS IS ATTACK BY STOMP");
        isAttack = true;


        yield return new WaitForSeconds(1f);
        isAttack = false;
        StateChange(AIState.Patrol);
    }

    void Dash()
    {

    }

    public void Rest()
    {
        isRest = true;
        damage = 0;
        _barrier.SetActive(true);
        Level4.LevelManager.instance.Phase();
        _anim.SetBool("isPatrol", false);
    }

    public void Unrest()
    {
        isRest = false;
        _barrier.SetActive(false);
    }
    #endregion

    IEnumerator Die()
    {
        _boomSound.Play();
        Instantiate(_dieEffect, transform.position, transform.rotation);
        _anim.SetTrigger("Death");
        _navMesh.isStopped = true;
        yield return new WaitForSeconds(1.5f);
        Level4.LevelManager.instance.Complete();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            _hp--;
            damage++;
        }
    }
}
