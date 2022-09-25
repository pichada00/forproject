using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class AI_leech : MonoBehaviour
{
    public TypeMonster type;
    public RangeMonster range;
    public Transform totem;
    NavMeshAgent agent;
    public Transform player;
    public Animator animator;
    State_leech currentState;
    //TextMeshProUGUI txtStatus;
    //Animator anim;

    public FieldOfView fieldOf;

    // Start is called before the first frame update
    void Start()
    {
        currentState = new Idle_leech(type, range, fieldOf, this.gameObject, agent, player, totem, animator);
    }

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        //txtStatus = this.GetComponentInChildren<TextMeshProUGUI>();
        player = GameObject.Find("CS Character Controller").GetComponent<Transform>();
        fieldOf = GetComponent<FieldOfView>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
        //anim.SetInteger("Walk", 1);
    }
}