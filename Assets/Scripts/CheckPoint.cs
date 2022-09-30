using System;
using System.Collections;
using System.Collections.Generic;
using DiasGames.Components;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> checkPoint;
    [SerializeField] private Vector3 vectorPoint;

    public Health health;

    //private Vector3 _spawnPoint;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            vectorPoint = player.transform.position;
            Destroy(other.gameObject);
        }
        
    }

}
