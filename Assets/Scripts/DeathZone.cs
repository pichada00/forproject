using DiasGames.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private NavMeshAgent aI = null;
    public Health health;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            health.Damage((int)health.MaxHP);
        }
    }
}
