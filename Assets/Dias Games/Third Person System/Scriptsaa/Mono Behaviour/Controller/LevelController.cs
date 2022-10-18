using System;
using UnityEngine;
using DiasGames.Components;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.AI;

namespace DiasGames.Controller
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private GameObject player = null;
        [SerializeField] private GameObject parent = null;
        [SerializeField] private NavMeshAgent aI = null;
        [SerializeField] private float delayToRestartLevel = 2f;

        [SerializeField] private UnityEvent OnCharacterRestart;
        private AbilityScheduler _scheduler = null;
        private Mover _mover = null;

        //public GameObject currentCheckPoint;
        private NewCheckPoint _newCheckPoint;

        // player components
        private Health _playerHealth;

        private CSPlayerController controller;

        // controller vars
        private bool _isRestartingLevel;

        private void Start()
        {
            _playerHealth = player.GetComponent<Health>();
            _newCheckPoint = GetComponent<NewCheckPoint>();
            //player.transform.position = GameManager.Instance.sceneInfostage1.currentCheckPointOfStageOne;
            //aI.transform.position = GameManager.Instance.sceneInfostage1.currentCheckPointOfAI;
        }
        private void Awake()
        {
            _scheduler = GetComponent<AbilityScheduler>();
            _mover = GameObject.FindGameObjectWithTag("Player").GetComponent<Mover>();
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player");

            if (aI == null)
                aI = GameObject.FindGameObjectWithTag("AIFriend").GetComponent<NavMeshAgent>();
            
            if(_playerHealth == null)
                _playerHealth = player.GetComponent<Health>();
            
            if(_newCheckPoint == null)
                _newCheckPoint = GetComponent<NewCheckPoint>();

            if(parent == null)
                parent = GameObject.FindGameObjectWithTag("Controlcharacter");

            if (controller == null)
                controller = player.GetComponent<CSPlayerController>();
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

            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            //Instantiate(GameManager.Instance.playergameObject, GameManager.Instance.sceneInfostage1.currentCheckPointOfStageOne, Quaternion.identity);

            //respawn



            player.transform.position = GameManager.Instance.sceneInfostage1.currentCheckPointOfStageOne;
            aI.Warp(player.transform.position + Vector3.left);
            Debug.Log(GameManager.Instance.sceneInfostage1.currentCheckPointOfStageOne);

            _playerHealth.RestoreFullHealth();

            

            if(player.transform.position == GameManager.Instance.sceneInfostage1.currentCheckPointOfStageOne)
            {
                OnCharacterRestart.Invoke();
            }
            //_playerHealth.onRestart += controller.restart;
            //_playerHealth.onRestart -= controller.Die;

            _isRestartingLevel = false;

            _playerHealth.animator.SetBool(_playerHealth.animtestDieState, false);
            Invoke("changebool", 0.1f);
            //Destroy(this.parent);
        }

        private void changebool()
        {
            _playerHealth.dead = false;
            _mover.enabled = true;
            Debug.Log(GameManager.Instance.sceneInfostage1.currentCheckPointOfStageOne);
        }
    }
}