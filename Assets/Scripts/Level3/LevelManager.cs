using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Level3
{
    public class LevelManager : MonoBehaviour
    {
        public static Level3.LevelManager instance;

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
            _scoreText.text = "Destroy all panel \n" + _score.ToString() + "/" + _keyCount.ToString();

            UserInterface.instance.StageStart();
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
            _scoreText.text = "Destroy all panel \n" + _score.ToString() + "/" + _keyCount.ToString();
        }

        public void Complete()
        {
            UserInterface.instance.StageClear("Level4");
        }
    }
}
