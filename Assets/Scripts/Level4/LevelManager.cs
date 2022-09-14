using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level4
{
    public class LevelManager : MonoBehaviour
    {
        public static Level4.LevelManager instance;

        [SerializeField] private GameObject _panelSet;
        [SerializeField] private GameObject _notification;
        private int targetPanel;
        private int panel;

        bool isComplete;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }

        void Start()
        {
            UserInterface.instance.StageStart();
        }

        public void Phase()
        {
            Instantiate(_panelSet, transform.position, transform.rotation);
            targetPanel = GameObject.FindGameObjectsWithTag("Key").Length;
            StartCoroutine(Notification());
        }

        IEnumerator Notification()
        {
            _notification.SetActive(true);
            yield return new WaitForSeconds(5f);
            _notification.SetActive(false);
        }

        public void GetScore()
        {
            panel++;
            if (panel >= targetPanel)
            {
                FindObjectOfType<Boss>().Unrest();
                panel = 0;
            }                
        }

        public void Complete()
        {
            if (isComplete) return;
            isComplete = true;

            GameManager.instance.StageEnd();
            UserInterface.instance.StageClear("Endgame");
            //Transition.instance.Fade(true, "Level2");
        }
    }
}
