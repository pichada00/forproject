using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class State_Buddy
{
    public enum StateStatus
    {
        Idle, Follow, Died
    };

    public enum EventState
    {
        Enter, Update, Exit
    };

    public StateStatus name;
    protected EventState stage;
    protected GameObject npc;
    protected Transform player;
    protected State_Buddy nextState;
    protected NavMeshAgent agent;
    //protected TextMeshProUGUI txtStatus;
    protected Animator animator;
    protected bool Aifollow;
    public StaminaController stamina;

    public State_Buddy(GameObject npc, NavMeshAgent agent, Transform player, Animator animator,bool aifollow,StaminaController stamina)
    {
        this.npc = npc;
        this.agent = agent;
        this.stage = EventState.Enter;
        this.player = player;
        //this.txtStatus = txtStatus;
        this.animator = animator;
        this.Aifollow = aifollow;
        this.stamina = stamina;
    }

    public virtual void Enter()
    {
        stage = EventState.Update;
    }

    public virtual void Update()
    {
        stage = EventState.Update;
    }

    public virtual void Exit()
    {
        stage = EventState.Exit;
    }

    public State_Buddy Process()
    {
        State_Buddy result = this;
        if (stage == EventState.Enter)
        {
            Enter();
        }

        else if (stage == EventState.Update)
        {
            Update();
        }

        if (stage == EventState.Exit)
        {
            Exit();
            result = nextState;
        }
        return result;
    }

    public bool changebool()
    {
        return true;
    }

    public bool changeboolfalse()
    {
        return false;
    }
    public float DistancePlayer()
    {
        return Vector3.Distance(npc.transform.position, player.position);
    }
}
