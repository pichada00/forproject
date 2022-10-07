using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging; 

public class keep : MonoBehaviour, IInteractable
{
    public Rigidbody rb;
    public Collider collider;
    public GameObject prefabGameObject;

    [SerializeField]private Transform PickUpPointR = null;
    [SerializeField]private Transform PickUpPointL = null;
    public MeshRenderer meshLambR;
    public MeshRenderer meshLambL;
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
    private void Awake()
    {
        meshLambR = GameObject.Find("lamb position R").transform.GetChild(0).GetComponent<MeshRenderer>();

        meshLambL = GameObject.Find("lamb position L").transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (keeped == true)
        {
            if (right == true)
            {
                DropR();
                return;
            }
            if (left == true)
            {
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
            /*PickUpPointL = GameObject.Find("Left_Hand_AI").transform;
            //interactor.handLeft = true;
            rb.useGravity = false;
            collider.enabled = false;
            this.gameObject.transform.position = PickUpPointL.position;
            this.transform.parent = GameObject.Find("Left_Hand_AI").transform;
            //keeped = true;
            AIkeeped = true;
            left = true;*/
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
        interactor.handLeft = true;
        keep lamb = GameObject.Find("lamb position L").GetComponent<keep>();
        meshLambL.enabled = true;
        Rig rig = GameObject.Find("openlamp hand L").GetComponent<Rig>();
        rig.weight = 1.0f;
        lamb.keeped = true;
        lamb.left = true;
        Destroy(gameObject);
        return true;
    }

    public bool InteractR(Interactor interactor)
    {
        interactor.handRight = true;
        keep lamb = GameObject.Find("lamb position R").GetComponent<keep>();
        MeshRenderer meshweapon = GameObject.Find("lamb position R").transform.GetChild(0).GetComponent<MeshRenderer>();
        Debug.Log(meshweapon.enabled);
        meshweapon.enabled = true;
        Rig rig = GameObject.Find("openlamp hand R").GetComponent<Rig>();
        rig.weight = 1.0f;
        lamb.keeped = true;
        lamb.right = true;
        Destroy(gameObject);
        return true;
    }
    private void DropR()
    {
        if (Input.GetKeyDown(KeyCode.E) && right == true)
        {
            prefabGameObject.gameObject.GetComponent<keep>().right = true;
            meshLambR.enabled = false;
            Rig rig = GameObject.Find("openlamp hand R").GetComponent<Rig>();
            rig.weight = 0.0f;
            keeped = false;
            Instantiate(prefabGameObject, this.gameObject.transform.position, Quaternion.identity);
            Invoke("ChangeBoolPick", 0.1f);
        }
        
        
    }
    private void DropL()
    {
        if (Input.GetKeyDown(KeyCode.Q) && left == true)
        {

            prefabGameObject.gameObject.GetComponent<keep>().left = true;
            Rig rig = GameObject.Find("openlamp hand L").GetComponent<Rig>();
            rig.weight = 0.0f;
            Instantiate(prefabGameObject, this.gameObject.transform.position, Quaternion.identity);
            meshLambL.enabled = false;
            keeped = false;
            Invoke("ChangeBoolPick", 0.1f);
        }
        
        
    }
    private void ChangeBoolPick()
    {
        if (right == true)
        {
            prefabGameObject.gameObject.GetComponent<keep>().right = false;
            Interactor.handRight = false;
            right = false;
            return;
        }
        if (left == true)
        {
            prefabGameObject.gameObject.GetComponent<keep>().left = false;
            Interactor.handLeft = false;
            Debug.Log(Interactor.handLeft);
            left = false;
            return;
        }
    }
}
