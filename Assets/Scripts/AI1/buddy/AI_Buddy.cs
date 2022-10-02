using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class AI_Buddy: MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Animator animator;
    public State_Buddy currentState;
    public StaminaController stamina;
    public bool aifollow;
    public bool followwithtotem = false;
    //TextMeshProUGUI txtStatus;
    //Animator anim;

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
        animator = GetComponent<Animator>();
        player = GameObject.Find("CS Character Controller").GetComponent<Transform>();
        stamina = GameObject.Find("CS Character Controller").GetComponent<StaminaController>();
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