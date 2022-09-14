using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Level1
{
    public class LevelManager : MonoBehaviour
    {
        public static Level1.LevelManager instance;

        [Header("REFERENCES")]
        [SerializeField] private int _score;

        [Header("USER INTERFACE")]
        [SerializeField] private TMP_Text _scoreText;

        private int _enemyCount;
        bool isComplete;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);

            _enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        }

        void Start()
        {                       
            UserInterface.instance.StageStart();
            _scoreText.text = "Destroy all robot \n" + _score.ToString() + "/" + _enemyCount.ToString();
        }

        void Update()
        {
            if (_score >= _enemyCount)
            {
                Complete();
            }
        }

        public void GetScore(int score)
        {
            _score += score;
            _scoreText.text = "Destroy all robot \n" + _score.ToString() + "/" + _enemyCount.ToString();
        }

        public void Complete()
        {
            if (isComplete) return;
            isComplete = true;

            GameManager.instance.StageEnd();
            UserInterface.instance.StageClear("Level2");
            //Transition.instance.Fade(true, "Level2");
        }
    }
}
