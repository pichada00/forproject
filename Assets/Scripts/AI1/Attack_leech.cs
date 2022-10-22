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
        if(range == RangeMonster.longdistance)
        {
            agent.stoppingDistance = 5.0f;
        }else
        {
            agent.stoppingDistance = 1.0f;
        }
        //agent.ResetPath();
    }

    public override void Enter()
    {
        time = 0;
        agent.transform.LookAt(player);
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
        switch (range)
        {
            case RangeMonster.longdistance:
                animator.CrossFadeInFixedTime("rangeattack", 0.1f);
                break;
            case RangeMonster.melee:
                animator.CrossFadeInFixedTime("Melee Attack Downward right", 0.1f);
                break;
        }
        Debug.Log("Attack");
        time += 1.0f * Time.deltaTime;
        if (((time > 4.5f) && range == RangeMonster.longdistance) || ((time > 1.25f) && range == RangeMonster.melee))//add in state
        {
            nextState = new Pursue_leech(type, range, fieldOf, npc, agent, player, totem, animator);
            stage = EventState.Exit;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
