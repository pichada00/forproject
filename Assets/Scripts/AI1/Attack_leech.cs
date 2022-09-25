using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Attack_leech : State_leech
{
    //private GameObject[] waypoints => WPManager.Instance.getWPPosition();
    float time = 0;

    public Attack_leech(TypeMonster type, RangeMonster range, FieldOfView fieldOf, GameObject npc, NavMeshAgent agent, Transform player, Transform totem, Animator animator) : base(type, range, fieldOf, npc, agent, player, totem, animator)
    {
        name = StateStatus.Attack;
        agent.speed = 0;
        agent.isStopped = false;
        agent.stoppingDistance = 0;
        agent.ResetPath();
    }

    public override void Enter()
    {
        time = 0;
        //txtStatus.text = "Attack";

        //playanimation


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
        if (time < 30)
        {
            time += 1f * Time.deltaTime;
        }else if(time >= 30)
        {
            nextState = new Idle_leech(type, range, fieldOf, npc, agent, player, totem, animator);
            stage = EventState.Exit;
        }
        /*if(agent.remainingDistance < 1)
        {
            if(currentIndex>=waypoints.Length - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            agent.SetDestination(waypoints[currentIndex].transform.position);

        }*/
        if (DistancePlayer() > 10)//add in state
        {
            nextState = new Pursue_leech(type, range, fieldOf,npc, agent, player, totem, animator);
            stage = EventState.Exit;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
