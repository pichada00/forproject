using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeMeleeWeapon { Wood, Iron}
public class WoodForAtk : MonoBehaviour, IInteractable
{
    public Rigidbody rb;
    public Collider collider;
    public new GameObject gameObject;

    [SerializeField] private Transform PickUpPointR = null;
    [SerializeField] private Transform PickUpPointL = null;
    [SerializeField] private BoxCollider boxCollider = null;
    public TypeMeleeWeapon typeMeleeWeapon;
    public interactsomething interactWood;
    public Interactor interactor;
    public Animator animator;
    public bool keeped = false;
    public bool AIkeeped = false;
    public bool right = false;
    public bool left = false;
    public bool shouldUse = false;
    public bool used = false;
    private float _time = 2f;
    public interactsomething interactsomething => interactWood;

    public string InteractionPrompt => throw new System.NotImplementedException();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        interactor = GameObject.Find("CS Character Controller").GetComponent<Interactor>();
        animator = GameObject.Find("CS Character Controller").GetComponent<Animator>();
        boxCollider = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider>();

    }
    private void Update()
    {
        if (keeped == true)
        {
            if (right == true)
            {
                gameObject.transform.position = PickUpPointR.position;
                if (_time == 2 && used == false)
                {
                    //Debug.Log(time);
                    UseItem(1);
                    if (shouldUse == true && typeMeleeWeapon == TypeMeleeWeapon.Wood)
                    {
                        Invoke("AfterUseItem", 2.5f);
                    }
                }

                if (used == true && _time > 0)
                {
                    _time -= 1 * Time.deltaTime;
                    //Debug.Log("time" + time);
                    if (_time <= 0)
                    {
                        ResetTime();
                    }
                }
                DropR();
                return;
            }
            if (left == true)
            {
                gameObject.transform.position = PickUpPointL.position;
                UseItem(0);
                if (shouldUse == true)
                {
                    UseItem(0);
                }
                DropL();
                return;
            }


        }

    }

    public void ResetTime()
    {
        used = false;
        _time = 2;
    }

    private void UseItem(int i)
    {
        if (Input.GetMouseButtonDown(i))
        {
            Debug.Log("useItem");
            switch (i)
            {
                case 0:
                    animator.CrossFadeInFixedTime("Melee Attack Downward left", 0.1f);
                    used = true;
                    Debug.Log("useItemleft");
                    break;
                case 1:
                    animator.CrossFadeInFixedTime("Melee Attack Downward right", 0.1f);
                    used = true;
                    Debug.Log("useItemright");
                    break;
            }
        }
    }

    public void AfterUseItem()
    {
        this.transform.parent = null;
        Debug.Log("afteruseitem");
        interactor.handRight = false;
        rb.useGravity = true;
        collider.enabled = true;
        keeped = false;
        Invoke("ChangeBoolPick", 1.0f);
    }

    public bool Interact(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }

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
            interactor.handRight = false;
            rb.useGravity = true;
            collider.enabled = true;
            keeped = false;
            Invoke("ChangeBoolPick", 1.0f);
        }


    }
    private void DropL()
    {
        if (Input.GetKeyDown(KeyCode.Q) && left == true)
        {
            this.transform.parent = null;
            interactor.handLeft = false;
            rb.useGravity = true;
            collider.enabled = true;
            keeped = false;
            Invoke("ChangeBoolPick", 1.0f);
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
        if (shouldUse == true)
        {
            Destroy(this.gameObject);
        }
    }

}
