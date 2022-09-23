using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialInstance : MonoBehaviour
{
    [SerializeField] private Renderer myObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myObject.material.color = Color.blue;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myObject.material.color = Color.red;
        }
    }
}
