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
    public float range = 10.0f;
    public bool canpull = false;

    private void Awake()
    {
        AIOnly = GameObject.Find("AI").GetComponent<NavMeshAgent>();
        AIrb = GameObject.Find("AI").GetComponent<Rigidbody>();
        AI = GameObject.Find("AI").GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canpull == true)
        {
            //playanimation
            AIrb.isKinematic = false;
            AIOnly.SetDestination(transformBottom.position);
            Invoke("teleporttoTop", 3.0f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Vector3.Distance(AIOnly.transform.position,transformBottom.position) <=range)
            {
                canpull = true;
            }
            
        }
    }

    private void teleporttoTop()
    {
        AIOnly.Warp(transformTop.position);
        AIrb.isKinematic = true;
        canpull = false;
    }
}
