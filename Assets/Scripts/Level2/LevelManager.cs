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
        [SerializeField] private GameObject _door;
        [SerializeField] private GameObject _doorCam;

        [Header("USER INTERFACE")]
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _doorText;

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
            _doorText.text = "KEY : " + (_keyCount - _score).ToString();
        }

        public IEnumerator Complete()
        {
            isComplete = true;
            _doorCam.SetActive(true);

            yield return new WaitForSeconds(1f);

            _door.LeanMoveLocalY(-4f, 0.5f);
            _doorText.text = "OPEN";
            _doorText.color = Color.green;

            yield return new WaitForSeconds(1f);

            _doorCam.SetActive(false);
        }
    }
}
