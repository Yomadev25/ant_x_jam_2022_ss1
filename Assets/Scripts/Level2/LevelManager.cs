using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Level2
{
    public class LevelManager : MonoBehaviour
    {
        public static Level2.LevelManager instance;

        [Header("REFERENCES")]
        [SerializeField] private int _score;

        [Header("USER INTERFACE")]
        [SerializeField] private TMP_Text _scoreText;

        private int _keyCount;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }

        private void Start()
        {
            _keyCount = GameObject.FindGameObjectsWithTag("Key").Length;
        }

        void Update()
        {
            if (_score >= _keyCount)
            {
                Complete();
            }
        }

        public void GetScore()
        {
            _score++;
        }

        public void Complete()
        {
            GameManager.instance.StageEnd();
        }
    }
}
