using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    public Transform player;
    
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask interactableMask;
    
    [SerializeField] private Renderer renderer;
    [SerializeField] private Collider collider;
    [SerializeField] private MeshCollider meshCollider;

    [SerializeField] private bool isLoaded;
    [SerializeField] private bool shouldLoad;

    private void Awake()
    {
        player = GameObject.Find("CS Character Controller").transform;
        renderer = this.gameObject.GetComponent<MeshRenderer>();
        collider = this.gameObject.GetComponent<Collider>();
        meshCollider = this.gameObject.GetComponent<MeshCollider>();
    }
    
    
    /*
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
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            renderer.material.color = Color.blue;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            renderer.material.color = Color.red;
        }
    }
}
