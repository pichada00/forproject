using System;
using UnityEngine;
using DiasGames.Components;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

namespace DiasGames.Controller
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private GameObject player = null;
        [SerializeField] private GameObject aI = null;
        [SerializeField] private float delayToRestartLevel = 3f;

        //public GameObject currentCheckPoint;
        private NewCheckPoint _newCheckPoint;

        // player components
        private Health _playerHealth;

        // controller vars
        private bool _isRestartingLevel;

        private void Start()
        {
            _playerHealth = player.GetComponent<Health>();
            _newCheckPoint = GetComponent<NewCheckPoint>();
            player.transform.position = GameManager.Instance.sceneInfostage1.currentCheckPointOfStageOne;
            aI.transform.position = GameManager.Instance.sceneInfostage1.currentCheckPointOfAI;
        }
        private void Awake()
        {
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player");

            if (aI == null)
                aI = GameObject.FindGameObjectWithTag("AIFriend");
            
            if(_playerHealth == null)
                _playerHealth = player.GetComponent<Health>();
            
            if(_newCheckPoint == null)
                _newCheckPoint = GetComponent<NewCheckPoint>();

        }

        private void OnEnable()
        {
            _playerHealth.OnDead += RestartLevel;
        }
        private void OnDisable()
        {
            _playerHealth.OnDead -= RestartLevel;
        }

        // Restarts the current level
        private void RestartLevel()
        {
            if (!_isRestartingLevel)
                StartCoroutine(OnRestart());
            //player.transform.position = _newCheckPoint.currentPlayerCheckPoint.position;
        }

        public void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        private IEnumerator OnRestart()
        {
            _isRestartingLevel = true;

            yield return new WaitForSeconds(delayToRestartLevel);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
            
            //respawn


            _isRestartingLevel = false;
        }
    }
}