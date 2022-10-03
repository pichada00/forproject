using System;
using System.Collections;
using System.Collections.Generic;
using DiasGames.Components;
using UnityEngine;

public class BoxCheckPoint : MonoBehaviour
{
    public NewCheckPoint checkPoint;
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
            checkPoint.currentPlayerCheckPoint = this.gameObject.transform;
            GameManager.Instance.sceneInfostage1.currentCheckPointOfStageOne = this.gameObject.transform.position;
            GameManager.Instance.sceneInfostage1.currentCheckPointOfAI = this.gameObject.transform.position - Vector3.back;
            Destroy(other.gameObject);
        }
    }
}
