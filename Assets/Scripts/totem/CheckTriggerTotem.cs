using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTriggerTotem : MonoBehaviour
{
    public Darktotem darkTotem;
    [SerializeField] private Renderer totem;
    private void Awake()
    {
        darkTotem = this.gameObject.GetComponentInParent<Darktotem>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Debug.Log("hit");
            this.gameObject.SetActive(false);
            darkTotem.totem2.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("weapon"))
        {
            Debug.Log("hitTrigger");
            darkTotem.GetComponent<Darktotem>();
            darkTotem.change = true;
            this.gameObject.SetActive(false);
            darkTotem.totem2.gameObject.SetActive(true);
        }

        /*if (other.CompareTag("Player"))
        {
            totem.material.color = Color.blue;
        }*/
    }

}
