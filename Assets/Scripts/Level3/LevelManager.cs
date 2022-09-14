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
        [SerializeField] private GameObject _door;
        [SerializeField] private GameObject _doorCam;
        [SerializeField] private AudioSource _doorSound;

        [Header("USER INTERFACE")]
        [SerializeField] private TMP_Text _scoreText;

        private int _keyCount;
        bool isComplete;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }

        IEnumerator Start()
        {
            _keyCount = GameObject.FindGameObjectsWithTag("Key").Length;
            _scoreText.text = "Destroy all panel \n" + _score.ToString() + "/" + _keyCount.ToString();

            yield return new WaitForSeconds(1f);
            _doorCam.SetActive(true);
            yield return new WaitForSeconds(2f);
            _doorCam.SetActive(false);

            UserInterface.instance.StageStart();
        }

        void Update()
        {
            if (_score >= _keyCount)
            {
                if (!isComplete) StartCoroutine(Complete());
            }
        }

        public void GetScore()
        {
            _score++;
            _scoreText.text = "Destroy all panel \n" + _score.ToString() + "/" + _keyCount.ToString();
        }

        public IEnumerator Complete()
        {
            isComplete = true;
            _doorCam.SetActive(true);
            _door.LeanMoveLocalY(-4f, 0.5f);
            _doorSound.Play();

            yield return new WaitForSeconds(1f);

            _doorCam.SetActive(false);
        }
    }
}
