using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class keep : MonoBehaviour, IInteractable
{
    public Rigidbody rb;
    public Collider collider;
    public GameObject gameObject;

    [SerializeField]private Transform PickUpPointR = null;
    [SerializeField]private Transform PickUpPointL = null;
    public interactsomething interactsomethingonITEM;
    public Interactor Interactor;

    

    public bool keeped = false;
    public bool AIkeeped = false;
    public bool right = false;
    public bool left = false;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        Interactor = GameObject.Find("CS Character Controller").GetComponent<Interactor>();
    }

    private void Update()
    {
        if (keeped == true)
        {
            if (right == true)
            {
                gameObject.transform.position = PickUpPointR.position;
                DropR();
                return;
            }
            if (left == true)
            {
                gameObject.transform.position = PickUpPointL.position;
                DropL();
                return;
            }
        }

        if(AIkeeped == true)
        {
            gameObject.transform.position = PickUpPointL.position;
            DropFromAI();
        }
        
    }

    

    private void DropFromAI()
    {
        if (Input.GetKeyDown(KeyCode.F) && AIkeeped == true)
        {
            this.transform.parent = null;
            Interactor.handRight = false;
            rb.useGravity = true;
            collider.enabled = true;
            AIkeeped = false;
            Invoke("ChangeBoolPick", 0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AIFriend"))
        {
            PickUpPointL = GameObject.Find("Left_Hand_AI").transform;
            //interactor.handLeft = true;
            rb.useGravity = false;
            collider.enabled = false;
            this.gameObject.transform.position = PickUpPointL.position;
            this.transform.parent = GameObject.Find("Left_Hand_AI").transform;
            //keeped = true;
            AIkeeped = true;
            left = true;
        }
    }

    
    public string InteractionPrompt => throw new System.NotImplementedException();

    interactsomething IInteractable.interactsomething => interactsomethingonITEM;

    public bool Interact(Interactor interactor)
    {
        //itemIsPickedR = true;
        throw new System.NotImplementedException();
    }   

    /*public bool Interact(ThirdPersonController thirdPersonController)
    {
        throw new System.NotImplementedException();
    }*/

    public bool InteractL(Interactor interactor)
    {

        PickUpPointL = interactor.PickUpPointL;
        interactor.handLeft = true;
        rb.useGravity = false;
        collider.enabled = false;
        this.gameObject.transform.position = interactor.PickUpPointL.position;
        this.transform.parent = GameObject.Find("Left_Hand").transform;
        keeped = true;
        left = true;
        return true;
    }

    public bool InteractR(Interactor interactor)
    {
        PickUpPointR = interactor.PickUpPointR;
        interactor.handRight = true;
        rb.useGravity = false;
        collider.enabled = false;
        this.gameObject.transform.position = interactor.PickUpPointR.position;
        this.transform.parent = GameObject.Find("Right_Hand").transform;
        keeped = true;
        right = true;
        return true;
    }
    private void DropR()
    {
        if (Input.GetKeyDown(KeyCode.E) && right == true)
        {
            this.transform.parent = null;
            Debug.Log("DropR");
            Interactor.handRight = false;
            rb.useGravity = true;
            collider.enabled = true;
            keeped = false;
            Invoke("ChangeBoolPick", 0.1f);
        }
        
        
    }
    private void DropL()
    {
        if (Input.GetKeyDown(KeyCode.Q) && left == true)
        {
            this.transform.parent = null;
            Interactor.handLeft = false;
            rb.useGravity = true;
            collider.enabled = true;
            keeped = false;
            Invoke("ChangeBoolPick", 0.1f);
        }
        
        
    }
    private void ChangeBoolPick()
    {
        if (right == true)
        {
            right = false;
            return;
        }
        if (left == true)
        {
            left = false;
            return;
        }
    }
}
