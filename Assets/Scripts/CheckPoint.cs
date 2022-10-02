using System;
using System.Collections;
using System.Collections.Generic;
using DiasGames.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ai;
    [SerializeField] private List<GameObject> checkPoint;
    [SerializeField] private float delayToRestartLevel = 3f;
    
    public GameObject currentCheckPoint;
    public GameObject newCheckPoint;

    //private Vector3 _checkPoint;
    //private Vector3 _positionPlayer;
    //private Vector3 _positionAi;
    private Health _playerHealth;
    
    private bool _isRestartingLevel;

    private void Awake()
    {
        if (player == null || ai == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            ai = GameObject.FindGameObjectWithTag("AIFriend");
        }
        _playerHealth = player.GetComponent<Health>();
    }
    
    private void OnEnable()
    {
        _playerHealth.OnDead += RestartLevel;
    }
    private void OnDisable()
    {
        _playerHealth.OnDead -= RestartLevel;
    }

    private void RestartLevel()
    {
        if (!_isRestartingLevel)
        {
            StartCoroutine(OnRestart());
        }
        player.transform.position = currentCheckPoint.transform.position;
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
        _isRestartingLevel = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            currentCheckPoint.transform.position = newCheckPoint.transform.position;
            Destroy(other.gameObject);
        }
    }
}
