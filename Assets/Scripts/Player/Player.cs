using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float _hp;
    public float _maxHp;

    private PlayerController _playerController;
    private PlayerShoot _playerShoot;
    private PlayerCamera _playerCamera;
    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _hitSound;

    bool isDie;

    void Start()
    {
        GameManager.instance.onStageStart += OnStart;
        GameManager.instance.onStageEnd += OnStop;

        _playerController = GetComponent<PlayerController>();
        _playerShoot = GetComponent<PlayerShoot>();
        _playerCamera = FindObjectOfType<PlayerCamera>();

        _maxHp = _hp;

        OnStop();
    }

    void Update()
    {
        if (_hp <= 0)
        {
            if (!isDie) StartCoroutine(Die());
        }
    }

    public void Heal(float heal)
    {
        _hp += heal;
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;
    }

    IEnumerator Die()
    {
        isDie = true;

        OnStop();
        _anim.SetTrigger("Die");
        //Effect

        yield return new WaitForSeconds(1.5f);

        Transition.instance.Fade(true, SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet") || other.CompareTag("BossBullet"))
        {
            TakeDamage(1f);
            _hitSound.Play();
        }
        if (other.CompareTag("EnemyMissile"))
        {
            TakeDamage(5f);
        }
    }

    void OnStart()
    {
        _playerController.enabled = true;
        _playerShoot.enabled = true;
        _playerCamera.enabled = true;
    }

    void OnStop()
    {
        _playerController.enabled = false;
        _playerShoot.enabled = false;
        _playerCamera.enabled = false;
    }
}
