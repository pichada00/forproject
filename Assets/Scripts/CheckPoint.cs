using System;
using System.Collections;
using System.Collections.Generic;
using DiasGames.Components;
using DiasGames.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> checkPoint;
    [SerializeField] public Vector3 respawnPoint;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("CheckPoint"))
            respawnPoint = player.transform.position;
        Destroy(other.gameObject);
    }
}
