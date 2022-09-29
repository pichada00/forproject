using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public enum TypeMonster{ normal, evolve }
public enum RangeMonster{ melee, longdistance }
public class State_leech
{
    public enum StateStatus
    {
        Idle, Pursue, Died, Attack,Stun
    };

    public enum EventState
    {
        Enter, Update, Exit
    };

    public StateStatus name;
    protected TypeMonster type;
    protected RangeMonster range;
    protected EventState stage;
    protected FieldOfView fieldOf;
    protected GameObject npc;
    protected Transform player;
    protected Transform totem;
    protected State_leech nextState;
    protected NavMeshAgent agent;
    //protected TextMeshProUGUI txtStatus;
    protected Animator animator;

    public State_leech(TypeMonster type, RangeMonster range, FieldOfView fieldOf, GameObject npc, NavMeshAgent agent, Transform player,Transform totem, Animator animator)
    {
        this.type = type;
        this.range = range;
        this.fieldOf = fieldOf;
        this.npc = npc;
        this.agent = agent;
        this.stage = EventState.Enter;
        this.player = player;
        this.totem = totem;
        //this.txtStatus = txtStatus;
        this.animator = animator;
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

    public State_leech Process()
    {
        State_leech result = this;
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

    public float DistancePlayer()
    {
        return Vector3.Distance(npc.transform.position, player.position);
    }

    public float DistanceTotem()
    {
        return Vector3.Distance(npc.transform.position, totem.position);
    }
}
