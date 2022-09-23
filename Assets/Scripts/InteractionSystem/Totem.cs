using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask interactableMask;
    
    [SerializeField] private Renderer myObject;
    
    private int _numFound;
    private readonly Collider[] _colliders = new Collider[2];
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
    private void LateUpdate()
    {
        _numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, _colliders,
            interactableMask);
        if (_numFound > 0)
        {
            
        }
    }

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
