using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Follow_Buddy : State_Buddy
{
    //private GameObject[] waypoints => WPManager.Instance.getWPPosition();
    int currentIndex = 0;
    public Follow_Buddy(GameObject npc, NavMeshAgent agent,Transform player,Animator animator,bool aifollow) :base(npc,agent,player,animator,aifollow)
    {
        name = StateStatus.Follow;
        agent.speed = 2.65f;
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
        agent.SetDestination(player.position);
        if (DistancePlayer()>=3.5)
        {

            
        }
        if (DistancePlayer() < 1.1 || Aifollow == false)
        {
            //agent.isStopped = true;
            animator.SetFloat("Speed", 0f);
            nextState = new Idle_Buddy(npc, agent, player, animator, Aifollow);
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
