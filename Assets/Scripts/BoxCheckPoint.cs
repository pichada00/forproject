using System;
using System.Collections;
using System.Collections.Generic;
using DiasGames.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxCheckPoint : MonoBehaviour
{
    public NewCheckPoint checkPoint;

    [SerializeField] private List<GameObject> parentCheckPoint;

    public Transform cube;
    
    private Health _playerHealth;

    private void Awake()
    {
        checkPoint = GetComponent<NewCheckPoint>();
        
        _playerHealth = GetComponent<Health>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            var o = this.gameObject;
            checkPoint.currentPlayerCheckPoint = o.transform;
            var position = o.transform.position;
            Debug.Log(SceneManager.GetActiveScene().name);

            switch (SceneManager.GetActiveScene().name)
            {
                case "Stage1Chapter1":                    
                    GameManager.Instance.sceneInfostage1.currentCheckPointOfStageOne = checkPoint.currentPlayerCheckPoint.position;
                    GameManager.Instance.sceneInfostage1.countcheckpointed++;
                    break;
                case "Stage1Chapter2":
                    GameManager.Instance.sceneInfostage1.currentCheckPointOfStageOne = checkPoint.currentPlayerCheckPoint.position;
                    GameManager.Instance.sceneInfostage1.countcheckpointed++;
                    break;
            }

            other.gameObject.SetActive(false);

        }
    }
}
