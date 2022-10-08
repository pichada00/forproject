using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[RequireComponent(typeof(AILinkMover))]
public class AI_Buddy: MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Animator animator;
    public State_Buddy currentState;
    public StaminaController stamina;
    public bool aifollow;
    public bool followwithtotem = false;
    private AILinkMover linkMover;
    //TextMeshProUGUI txtStatus;
    //Animator anim;

    private const string Jump = "Air.Jump";

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        
        
        currentState = new Idle_Buddy(this.gameObject, agent, player, animator, aifollow,stamina);
        //txtStatus = this.GetComponentInChildren<TextMeshProUGUI>();


    }
    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        linkMover = GetComponent<AILinkMover>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("CS Character Controller").GetComponent<Transform>();
        stamina = GameObject.Find("CS Character Controller").GetComponent<StaminaController>();

        linkMover.OnLinkStart += HandleLinkStart;
        linkMover.OnLinkEnd += HandleLinkEnd;
    }

    private void HandleLinkStart()
    {
        //animation jump
        //animator.CrossFadeInFixedTime(Jump, 0.1f);
    }
    private void HandleLinkEnd()
    {
        //animation jump
    }

    // Update is called once per frame
    void Update()
    {        
        currentState = currentState.Process();
        if(Vector3.Distance(player.position,this.transform.position) >= 10)
        {
               
        }
        //anim.SetInteger("Walk", 1);
    }
}