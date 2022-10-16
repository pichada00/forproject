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
    public GameObject jump;
    public AI_Buddy aI;

    private void Awake()
    {
        AIOnly = GameObject.Find("AI").GetComponent<NavMeshAgent>();
        player = GameObject.Find("CS Character Controller").GetComponent<NavMeshAgent>();
        AIrb = GameObject.Find("AI").GetComponent<Rigidbody>();
        AI = GameObject.Find("AI").GetComponent<Transform>();
        animator = GameObject.Find("AI").GetComponent<Animator>();
        aI = GameObject.Find("AI").GetComponent<AI_Buddy>();
        animatorplayer = GameObject.Find("CS Character Controller").GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Vector3.Distance(AIOnly.transform.position,transformBottom.position) <=range)
            {
                canpull = true;
            }
            else
            {
                canpull = false;
            }

            if (Input.GetKeyDown(KeyCode.Q) && canpull == true)
            {
                jump.SetActive(true);
                aI.aifollow = true;
                aI.currentState = new Idle_Buddy(this.gameObject, aI.agent, aI.player, aI.animator, aI.aifollow, aI.stamina);

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
