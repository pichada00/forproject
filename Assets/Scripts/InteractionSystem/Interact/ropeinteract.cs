using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeinteract : MonoBehaviour, IInteractable
{
    public interactsomething interactshand;
    public GameObject ropeopen;
    public interactsomething interactsomething => interactshand;

    public string InteractionPrompt => throw new System.NotImplementedException();

    public bool Interact(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }

    public bool InteractL(Interactor interactor)
    {
        this.gameObject.SetActive(false);
        ropeopen.SetActive(true);
        interactor.handLeft = false;
        return true;
    }

    public bool InteractR(Interactor interactor)
    {
        this.gameObject.SetActive(false);
        ropeopen.SetActive(true);
        interactor.handRight = false;
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
