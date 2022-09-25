using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Pursue_leech : State_leech
{
    //private GameObject[] waypoints => WPManager.Instance.getWPPosition();
    int currentIndex = 0;
    public Pursue_leech(TypeMonster type, RangeMonster range, FieldOfView fieldOf, GameObject npc, NavMeshAgent agent, Transform player, Transform totem, Animator animator) : base(type, range, fieldOf, npc, agent, player, totem, animator)
    {
        name = StateStatus.Pursue;
        agent.speed = 2;
        agent.isStopped = false;
        agent.stoppingDistance = 1.0f;
        agent.ResetPath();
    }

    public override void Enter()
    {
        //txtStatus.text = "pursue";

        /*float lastDist = Mathf.Infinity;
        currentIndex = 0;
        for(int i = 0; i < waypoints.Length; i++)
        {
            GameObject thisWp = waypoints[i];
            float distance = Vector3.Distance(thisWp.transform.position, npc.transform.position);
            if (distance < lastDist)
            {
                currentIndex = i - 1;
                lastDist = distance;
            }
        }*/
        base.Enter();
    }

    public override void Update()
    {
        animator.SetFloat("Speed", 0.11f);
        agent.SetDestination(player.position);
        if(DistanceTotem() >= 10)
        {
            nextState = new Idle_leech(type, range, fieldOf, npc, agent, player, totem, animator);
            stage = EventState.Exit;
        }

        /////
        switch (type)
        {
            case TypeMonster.normal:
                switch (range)
                {
                    case RangeMonster.melee:
                        if (DistancePlayer() < 1)//add in state
                        {
                            animator.SetFloat("Speed", 0f);
                            nextState = new Attack_leech(type, range, fieldOf, npc, agent, player, totem, animator);
                            stage = EventState.Exit;
                        }
                        break;
                    case RangeMonster.longdistance:
                        if (DistancePlayer() < 5)//add in state
                        {
                            animator.SetFloat("Speed", 0f);
                            nextState = new Attack_leech(type, range, fieldOf, npc, agent, player, totem, animator);
                            stage = EventState.Exit;
                        }
                        break;
                }

                break;
            case TypeMonster.evolve:
                switch (range)
                {
                    case RangeMonster.melee:
                        if (DistancePlayer() < 1)//add in state
                        {
                            animator.SetFloat("Speed", 0f);
                            nextState = new Attack_leech(type, range, fieldOf, npc, agent, player, totem, animator);
                            stage = EventState.Exit;
                        }
                        break;
                    case RangeMonster.longdistance:
                        if (DistancePlayer() < 5)//add in state
                        {
                            animator.SetFloat("Speed", 0f);
                            nextState = new Attack_leech(type, range, fieldOf, npc, agent, player, totem, animator);
                            stage = EventState.Exit;
                        }
                        break;
                }
                break;
        }
        /////
    }

    public override void Exit()
    {
        base.Exit();
    }
}
