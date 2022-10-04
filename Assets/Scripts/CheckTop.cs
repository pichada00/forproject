using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckTop : MonoBehaviour
{
    public Rotationsolve rotationsolve;

    public void OnEnable()
    {
        gameObject.SetActive(true);
    }

    public void OnDisable()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        rotationsolve.OnEnable();
        OnDisable();
    }
}
