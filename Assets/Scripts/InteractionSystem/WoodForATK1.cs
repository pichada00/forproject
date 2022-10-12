using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeMeleeWeapon { Nothing, Wood, Iron }
public class WoodForATK1 : MonoBehaviour, IInteractable
{
    public Rigidbody rb;
    public Collider collider;
    //public GameObject gameObject;

    [SerializeField] private Transform PickUpPointR = null;
    [SerializeField] private Transform PickUpPointL = null;
    [SerializeField] private Collider boxCollider = null;
    public TypeMeleeWeapon typeMeleeWeapon;
    public interactsomething interactWood;
    public Interactor interactor;
    public Animator animator;
    public GameObject weaponSpawn;
    public bool keeped = false;
    public bool AIkeeped = false;
    public bool right = false;
    public bool left = false;
    public bool shouldUse = false;
    public bool used = false;
    private float _time = 1.25f;
    public interactsomething interactsomething => interactWood;

    public string InteractionPrompt => throw new System.NotImplementedException();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider =  GetComponent<Collider>();
        //gameObject = GetComponent<GameObject>();
        interactor = GameObject.Find("CS Character Controller").GetComponent<Interactor>();
        animator = GameObject.Find("CS Character Controller").GetComponent<Animator>();
        boxCollider = this.gameObject.transform.GetChild(0).GetComponent<Collider>();

    }
    private void Update()
    {
        if (keeped == true)
        {
            if (right == true)
            {
                if (_time == 1.25f && used == false)
                {
                    //Debug.Log(time);
                    UseItem(1);                    
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
                if (_time == 1.25f && used == false)
                {
                    //Debug.Log(time);
                    UseItem(0);
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
                DropL();
                return;
            }


        }

    }

    public void ResetTime()
    {
        used = false;
        _time = 1.25f;
    }

    private void UseItem(int i)
    {
        if (Input.GetMouseButtonDown(i))
        {
            Debug.Log("useItem");
            switch (i)
            {
                case 0:
                    animator.applyRootMotion = true;
                    animator.CrossFadeInFixedTime("Melee Attack Downward left", 0.1f);
                    used = true;
                    Debug.Log("useItemleft");
                    break;
                case 1:
                    animator.applyRootMotion = true;
                    animator.CrossFadeInFixedTime("Melee Attack Downward right", 0.1f);
                    used = true;
                    Debug.Log("useItemright");
                    break;
            }
            if (typeMeleeWeapon == TypeMeleeWeapon.Wood)
            {
                Invoke("AfterUseItem", 1.5f);
            }
        }
    }

    public void AfterUseItem()
    {
        if(right == true)
        {
            interactor.handRight = false;
            MeshRenderer meshweapon = GameObject.Find("weaponR").transform.GetChild(1).GetComponent<MeshRenderer>();
            meshweapon.enabled = false;
        }
        else if( left == true)
        {
            interactor.handLeft = false; 
            MeshRenderer meshweapon = GameObject.Find("weaponL").transform.GetChild(1).GetComponent<MeshRenderer>();
            meshweapon.enabled = false;
        }
        keeped = false;
        Invoke("ChangeBoolPick", 1.0f);
    }

    public bool Interact(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }

    public bool InteractL(Interactor interactor)
    {
        interactor.handLeft = true;
        WoodForATK1 weapon = GameObject.Find("weaponL").GetComponent<WoodForATK1>();
        MeshRenderer meshweapon = GameObject.Find("weaponL").transform.GetChild(1).GetComponent<MeshRenderer>();
        meshweapon.enabled = true;
        weapon.typeMeleeWeapon = typeMeleeWeapon;
        weapon.keeped = true;
        weapon.left = true;
        Destroy(gameObject);
        return true;
    }

    public bool InteractR(Interactor interactor)
    {
        interactor.handRight = true;
        WoodForATK1 weapon = GameObject.Find("weaponR").GetComponent<WoodForATK1>();
        MeshRenderer meshweapon = GameObject.Find("weaponR").transform.GetChild(1).GetComponent<MeshRenderer>();
        meshweapon.enabled = true;
        weapon.typeMeleeWeapon = typeMeleeWeapon;
        weapon.keeped = true;
        weapon.right = true;
        Destroy(gameObject);
        return true;
    }

    private void DropR()
    {
        if (Input.GetKeyDown(KeyCode.E) && right == true)
        {
            Instantiate(weaponSpawn, this.gameObject.transform.position, Quaternion.identity);
            MeshRenderer meshweapon = GameObject.Find("weaponR").transform.GetChild(1).GetComponent<MeshRenderer>();
            meshweapon.enabled = false;
            interactor.handRight = false;
            keeped = false;
            ChangeBoolPick();
        }


    }
    private void DropL()
    {
        if (Input.GetKeyDown(KeyCode.Q) && left == true)
        {
            Instantiate(weaponSpawn, this.gameObject.transform.position, Quaternion.identity); 
            MeshRenderer meshweapon = GameObject.Find("weaponL").transform.GetChild(1).GetComponent<MeshRenderer>();
            meshweapon.enabled = false;
            interactor.handLeft = false;
            keeped = false;
            ChangeBoolPick();
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
