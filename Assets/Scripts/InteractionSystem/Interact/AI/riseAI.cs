using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.AI;

public class riseAI : MonoBehaviour
{
    public Transform transformbackplayer;
    public Transform transformbottom;
    public Transform player;
    public Transform transformTop;
    public Transform aiwalkto;

    public Transform AI;
    public Animator animator;
    public NavMeshAgent AIOnly;
    public Rigidbody AIrb;
    public Rotationsolve rotationsolve;
    public CheckTop checkTop;
    public Rig[] rigs;


    public bool AIclimb = false;
    public bool AIcanclimb = false;
    public float currentpointy;
    public float range = 20.0f;

    private void Awake()
    {
        transformbackplayer = GameObject.Find("pointtoclimbofAI").GetComponent<Transform>();
        transformbottom = transform.GetChild(1);
        rotationsolve = gameObject.transform.GetChild(2).GetComponent<Rotationsolve>();
        checkTop = gameObject.transform.GetChild(3).GetComponent<CheckTop>();
        player = GameObject.Find("CS Character Controller").GetComponent<Transform>();
        AIOnly = GameObject.Find("AI").GetComponent<NavMeshAgent>();
        AIrb = GameObject.Find("AI").GetComponent<Rigidbody>();
        AI = GameObject.Find("AI").GetComponent<Transform>();
        animator = GameObject.Find("AI").GetComponent<Animator>();
        aiwalkto = GameObject.Find("aiwalkto").GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(AIclimb == false)
        {
            if (currentpointy != player.position.y)
            {
                currentpointy = player.position.y;
            }
        }

        if (AIcanclimb == true) 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("movem");
                //playanimation
                AIrb.isKinematic = false;
                AIrb.useGravity = false;
                AIclimb = true;
                rotationsolve.OnDisable();
                checkTop.OnEnable();
                //AIOnly.Warp(transformbackplayer.position);
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            AIclimb = false;
            AIOnly.enabled = true;
            rigs[0].weight = 0;
            rigs[1].weight = 0;
            rigs[2].weight = 0;
        }
            
        if (AIclimb == true)
        {
            AIOnly.Warp(aiwalkto.position);
            AIOnly.enabled = false;
            rigs[0].weight = 1;
            rigs[1].weight = 1;
            rigs[2].weight = 1;

        }
        else
        {
            
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && AIclimb == false)
        {
            Debug.Log("movemfalse");
            Debug.Log(Vector3.Distance(AIOnly.transform.position, transformbottom.position));
            if (Vector3.Distance(AIOnly.transform.position,transformbottom.position) <= range)
            {
                AIcanclimb = true;                
            }            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && AIclimb == true)
        {
            //playanimation
            AIclimb = false;
            AIcanclimb = false;
            AIOnly.enabled = true;
            rotationsolve.OnEnable();
            rigs[0].weight = 0;
            rigs[1].weight = 0;
            rigs[2].weight = 0;
            //Invoke("teleporttoTop", 3.0f);            
        }
    }
    private void teleporttoTop()
    {
        AIOnly.Warp(transformTop.position);
        AIrb.isKinematic = true;
        AIrb.useGravity = true;
    }
}
