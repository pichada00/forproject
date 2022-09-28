using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passiveAI : MonoBehaviour, IInteractable
{
    public interactsomething interactsomethingofAI;
    public Interactor Interactor;
    public AI_Buddy aI;

    private void Awake()
    {
        aI = GameObject.Find("AI").GetComponent<AI_Buddy>();
        Interactor = GameObject.Find("CS Character Controller").GetComponent<Interactor>();
    }
    public interactsomething interactsomething => interactsomethingofAI;

    public string InteractionPrompt => throw new System.NotImplementedException();

    public bool Interact(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }

    public bool InteractL(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }

    public bool InteractR(Interactor interactor)
    {
        if(aI.followwithtotem == false)
        {
            switch (aI.aifollow)
            {
                case false:
                    aI.aifollow = true;
                    aI.currentState = new Idle_Buddy(this.gameObject, aI.agent, aI.player, aI.animator, aI.aifollow);
                    break;
            }
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aI.aifollow == true)
        {
            /*if (Input.GetMouseButtonDown(1))
            {
                aI.currentState = new Follow_Buddy(this.gameObject, aI.agent, aI.player, aI.animator, aI.stamina, aI.aifollow);
            }*/
        }

        if (Input.GetKeyDown(KeyCode.E) && aI.aifollow == true)
        {
            aI.aifollow = false;
            aI.currentState = new Idle_Buddy(this.gameObject, aI.agent, aI.player, aI.animator, aI.aifollow);
            Interactor.handRight = false;
        }
        
    }
}
