using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Idle_Buddy : State_Buddy
{
    //private GameObject[] hides => HideManager.Instance.getHidePosition();
    public Idle_Buddy(GameObject npc, UnityEngine.AI.NavMeshAgent agent, Transform player,Animator animator, bool aifollow, StaminaController stamina) : base(npc, agent, player,animator, aifollow, stamina)
    {
        name = StateStatus.Idle;
        //agent.speed = 12;
        agent.isStopped = false;
        aifollow = this.Aifollow;

        //agent.ResetPath();

    }

    public override void Enter()
    {
        //txtStatus.text = "Idle";
        base.Enter();
    }

    public override void Update()
    {
        //playanimation or patrol
        if (Aifollow == true)
        {
            nextState = new Follow_Buddy(npc, agent, player, animator, Aifollow,stamina);
            stage = EventState.Exit;
        }
        //Hide();
        if (Aifollow == true)
        {
            
        }
        
    }

    public override void Exit()
    {
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
