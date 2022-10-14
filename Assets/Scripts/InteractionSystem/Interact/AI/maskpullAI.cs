using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class maskpullAI : MonoBehaviour
{
    public Transform transformBottom;
    public Transform transformTop;

    public Animator animator;
    public Animator animatorplayer;
    public Transform AI;
    public NavMeshAgent AIOnly;
    public NavMeshAgent player;
    public Rigidbody AIrb;
    public Collider collider;
    public float range = 10.0f;
    public bool canpull = false;

    private void Awake()
    {
        AIOnly = GameObject.Find("AI").GetComponent<NavMeshAgent>();
        player = GameObject.Find("CS Character Controller").GetComponent<NavMeshAgent>();
        AIrb = GameObject.Find("AI").GetComponent<Rigidbody>();
        AI = GameObject.Find("AI").GetComponent<Transform>();
        animator = GameObject.Find("AI").GetComponent<Animator>();
        animatorplayer = GameObject.Find("CS Character Controller").GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canpull == true)
        {
            //playanimation
            AIrb.isKinematic = false;
            player.enabled = true;
            AIOnly.SetDestination(transformBottom.position);
            player.SetDestination(transformTop.position);
            Invoke("aniamtiona", 2.0f);

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

    private void aniamtiona()
    {
        animator.CrossFadeInFixedTime("pull up_White", 0.1f);
        animatorplayer.CrossFadeInFixedTime("pull up", 0.1f);
        Invoke("teleporttoTop", 2.0f);
    }

    private void teleporttoTop()
    {
        AIOnly.Warp(transformTop.position);
        AIrb.isKinematic = true;
        canpull = false;

        player.enabled = false;
        collider.enabled = false;
    }
}
