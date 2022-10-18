using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Follow_Buddy : State_Buddy
{
    //private GameObject[] waypoints => WPManager.Instance.getWPPosition();
    
    //int currentIndex = 0;
    public Follow_Buddy(GameObject npc, NavMeshAgent agent,Transform player,Animator animator,bool aifollow,StaminaController stamina) :base(npc,agent,player,animator,aifollow,stamina)
    {
        name = StateStatus.Follow;
        agent.speed = 2.6f;
        agent.isStopped = false;
        agent.stoppingDistance = 1.0f;
        agent.ResetPath();
    }

    public override void Enter()
    {
        
        base.Enter();
    }

    public override void Update()
    {
        

        //Debug.Log(DistancePlayer());

        if (DistancePlayer() > 1.1)
        {
            agent.speed = 2.65f;
            agent.SetDestination(player.position);
            animator.SetFloat("Speed", 2.65f);
        }
        if(stamina.weAreSprinting == true)
        {
            agent.speed = 5.2f;
            animator.SetFloat("Speed", 5.3f);
        }

        if (DistancePlayer() < 1.1f || Aifollow == false)
        {
            //agent.isStopped = true;
            animator.SetFloat("Speed", 0f);
            nextState = new Idle_Buddy(npc, agent, player, animator, Aifollow,stamina);
            stage = EventState.Exit;
        }
        /*if (DistancePlayer() < 1)//add in state
        {
            nextState = new Attack_leech (npc, agent, player, txtStatus,animator);
            stage = EventState.Exit;
        }*/
    }

    public override void Exit()
    {
        base.Exit();
    }
}
