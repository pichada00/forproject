using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stun_leech : State_leech
{
    //private GameObject[] hides => HideManager.Instance.getHidePosition();
    float timestun;
    public Stun_leech(TypeMonster type, RangeMonster range, FieldOfView fieldOf, GameObject npc, NavMeshAgent agent, Transform player, Transform totem, Animator animator) : base(type, range, fieldOf, npc, agent, player, totem, animator)
    {
        name = StateStatus.Stun;
        //agent.speed = 12;
        agent.isStopped = true;
        //agent.ResetPath();
    }

    public override void Enter()
    {
        //txtStatus.text = "Idle";
        animator.SetFloat("Speed", 0f);
        animator.CrossFadeInFixedTime("stun", 0.1f);
        timestun = 2f;
        base.Enter();
    }

    public override void Update()
    {
        //playanimation or stun
        if (timestun > 0)
        {
            timestun -= 0.5f*Time.deltaTime;
            return;
        }

        nextState = new Idle_leech(type, range, fieldOf, npc, agent, player, totem, animator);
        stage = EventState.Exit;
        
    }

    public override void Exit()
    {
        animator.CrossFadeInFixedTime("Grounded.Idle", 0.1f);
        base.Exit();
    }

    /*public void Hide()
    {
        float farFactor = 20;
        float lastDist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        for (int i = 0; i < hides.Length; i++)
        {
            Vector3 hideDir = hides[i].transform.position - player.transform.position;
            Vector3 hidePos = hides[i].transform.position + hideDir.normalized * farFactor;

            float dist = Vector3.Distance(npc.transform.position, hidePos);
            if (dist < lastDist)
            {
                chosenSpot = hidePos;
                lastDist = dist;
            }
        }
        agent.SetDestination(chosenSpot);
    }*/
}
