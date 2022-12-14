using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private GameObject _effect;
    [SerializeField] private AudioSource _sound;
    Level2.LevelManager _levelManager;

    private void Start()
    {
        _levelManager = FindObjectOfType<Level2.LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _levelManager.GetScore();
            Instantiate(_effect, this.transform.position, this.transform.rotation);
            _sound.Play();
            Destroy(this.gameObject);            
        }
    }
}
