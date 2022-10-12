using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotationsolve : MonoBehaviour
{
    public Transform transform;
    public CheckTop checkTop;
    public riseAI rise;
    public int i;

    public void OnEnable()
    {
        gameObject.SetActive(true);
    }

    public void OnDisable()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 lookrotation = transform.position;
            other.transform.rotation = Quaternion.LookRotation(lookrotation);
            checkTop.OnEnable();
            if(i == 1)
            {
                rise.AIclimb = true;
            }
            OnDisable();
        }
        
    }
}
