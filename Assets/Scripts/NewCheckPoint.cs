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

    public Transform playerPosition;
    public Transform aiPosition;
    public Transform currentPlayerCheckPoint;
    public Transform currentAICheckPoint;
    public Transform checkPointPlayer;
    public Transform checkPointAI;

    private void Start()
    {
        currentPlayerCheckPoint = checkPointPlayer;
        currentAICheckPoint = checkPointAI;
        
        //Respawn();
    }
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ai = GameObject.FindGameObjectWithTag("AIFriend");

        hp = GetComponent<Health>();
        
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        aiPosition = GameObject.FindGameObjectWithTag("AIFriend").GetComponent<Transform>();
        checkPointPlayer = GameObject.FindGameObjectWithTag("FirstRespawn").GetComponent<Transform>();
        checkPointAI = GameObject.FindGameObjectWithTag("FirstRespawnAI").GetComponent<Transform>();
    }

    private void Respawn()
    {
        playerPosition.position = currentPlayerCheckPoint.position;
        aiPosition.position = currentAICheckPoint.position;
    }
}
