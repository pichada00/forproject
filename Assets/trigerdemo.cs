using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigerdemo : MonoBehaviour
{
    public GameObject panelDemo;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            panelDemo.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
