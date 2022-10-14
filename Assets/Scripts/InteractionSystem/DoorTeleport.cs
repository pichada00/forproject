using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorTeleport : MonoBehaviour
{
    public UnityEvent unityEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            unityEvent.Invoke();
        }
    }
}
