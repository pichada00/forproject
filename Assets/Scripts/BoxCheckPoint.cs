using System;
using System.Collections;
using System.Collections.Generic;
using DiasGames.Components;
using UnityEngine;

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
            GameManager.Instance.sceneInfostage1.currentCheckPointOfStageOne = position;
            GameManager.Instance.sceneInfostage1.currentCheckPointOfAI = position - Vector3.back;
            Destroy(other.gameObject);
        }
    }
}
