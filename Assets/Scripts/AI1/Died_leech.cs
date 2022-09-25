using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Died_leech : State_leech
{
    public Died_leech(TypeMonster type, RangeMonster range, GameObject npc, NavMeshAgent agent, Transform player, Transform totem, Animator animator) : base(type, range, npc, agent, player, totem, animator)
    {
        name = StateStatus.Died;
        agent.speed = 2;
        agent.isStopped = false;
        agent.stoppingDistance = 0;
        agent.ResetPath();
    }
    public override void Enter()
    {
        //txtStatus.text = "Died_leech";
        base.Enter();
    }

    public override void Update()
    {
        //died()
        /*Evade();
        if (DistancePlayer() > 20)
        {
            nextState = new Hide_Cat (npc, agent, player, txtStatus);
            stage = EventState.Exit;
        }*/
    }

    /*public void Flee(Vector3 location)
    {
        agent.SetDestination((npc.transform.position - location + npc.transform.position));
    }

    public void Evade()
    {
        Vector3 targetDir = player.transform.position - npc.transform.position;
        float playerSpeed = player.GetComponent<NavMeshAgent>().velocity.magnitude;
        float lookAhead = targetDir.magnitude / (agent.speed + playerSpeed);
        Flee(player.transform.position + player.transform.forward * lookAhead);
    }*/

    public override void Exit()
    {
        base.Exit();
    }
}
