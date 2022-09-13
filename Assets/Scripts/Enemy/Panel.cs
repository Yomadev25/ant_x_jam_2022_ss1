using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    [SerializeField] private float _hp;
    private float _maxHp;
    [SerializeField] private GameObject _effect;
    private Transform _targetPos;
    [SerializeField] private GameObject _enemySet;

    [Header("HEALTH BAR")]
    [SerializeField] private GameObject _hpPanel;
    [SerializeField] private CanvasGroup _hpCanvasGroup;
    [SerializeField] private Image _hpBar;

    [Header("SOUND EFFECT")]
    [SerializeField] private AudioSource _gunSound;
    [SerializeField] private AudioSource _hitSound;


    bool isDie;

    void Start()
    {
        _maxHp = _hp;
        _targetPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (_hp <= 0)
        {
            if (isDie) return;
            isDie = true;
            Die();
        }

        _hpBar.fillAmount = _hp / _maxHp;
        _hpPanel.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        if (Vector3.Magnitude(this.transform.position - _targetPos.transform.position) <= 20f) _hpCanvasGroup.alpha = 1;
        else _hpCanvasGroup.alpha = 0;
    }

    void Die()
    {
        FindObjectOfType<Level3.LevelManager>().GetScore();
        _enemySet.SetActive(true);
        Instantiate(_effect, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            _hp--;
            _hitSound.Play();
        }
    }
}
