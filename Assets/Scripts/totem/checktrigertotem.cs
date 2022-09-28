using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checktrigertotem : MonoBehaviour
{
    public Darktotem darktotem;
    private void Awake()
    {
        darktotem = this.gameObject.GetComponentInParent<Darktotem>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            Debug.Log("hit");
            this.gameObject.SetActive(false);
            darktotem.totem2.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "weapon")
        {
            Debug.Log("hittriger");
            this.gameObject.SetActive(false);
            darktotem.totem2.gameObject.SetActive(true);
        }
    }
}
