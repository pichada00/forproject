using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnWaeapon : MonoBehaviour
{
    public Transform[] positionSpawn;
    public GameObject prefabWeapon;

    private void Awake()
    {
        for(int i = 0; i <= positionSpawn.Length - 1; i++)
        {
            Instantiate(prefabWeapon, positionSpawn[i].position, Quaternion.identity);
        }
    }
}
