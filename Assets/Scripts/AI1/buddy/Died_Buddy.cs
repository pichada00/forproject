using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Died_Buddy : State_Buddy
{ 
    public Died_Buddy(GameObject npc, UnityEngine.AI.NavMeshAgent agent, Transform player,Animator animator,bool aifollow) : base(npc, agent, player,animator,aifollow)
    {
        name = StateStatus.Died;
        agent.speed = 12;
        agent.isStopped = false;
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
