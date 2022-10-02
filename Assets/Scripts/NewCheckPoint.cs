using System;
using System.Collections;
using System.Collections.Generic;
using DiasGames.Components;
using UnityEngine;

public class NewCheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ai;

    public List<GameObject> checkPoint;

    public Health hp;

    private Vector3 _playerPosition;
    private Vector3 _aiPosition;
    private Vector3 _currentPlayerCheckPoint;
    private Vector3 _currentAICheckPoint;
    private Vector3 _checkPointPlayer;
    private Vector3 _checkPointAI;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ai = GameObject.FindGameObjectWithTag("AIFriend");

        hp = player.GetComponent<Health>();
        var position = player.transform.position;
        _currentPlayerCheckPoint = position;
        _currentAICheckPoint = position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            _checkPointPlayer = gameObject.transform.position;
            Destroy(other.gameObject);
        }
    }
}
