using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class maskpullAI : MonoBehaviour
{
    public Transform transformBottom;
    public Transform transformTop;

    public Transform AI;
    public NavMeshAgent AIOnly;
    public Rigidbody AIrb;
    public float range = 5.0f;

    private void Awake()
    {
        AIOnly = GameObject.Find("AI").GetComponent<NavMeshAgent>();
        AIrb = GameObject.Find("AI").GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Vector3.Distance(AIOnly.transform.position,transformBottom.position) <=range)
            {
                if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
                {
                    //playanimation
                    AIrb.isKinematic = false;
                    AIOnly.SetDestination(transformBottom.position);
                    Invoke("teleporttoTop", 3.0f);
                }
            }
            
        }
    }

    private void teleporttoTop()
    {
        AIOnly.Warp(transformTop.position);
        AIrb.isKinematic = true;
    }
}
