using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class riseAI : MonoBehaviour
{
    public Transform transformbackplayer;
    public Transform transformbottom;
    public Transform player;
    public Transform transformTop;

    public Transform AI;
    public NavMeshAgent AIOnly;
    public Rigidbody AIrb;

    public bool AIclimb = false;
    public bool AIcanclimb = false;
    public float currentpointy;
    public float range = 20.0f;

    private void Awake()
    {
        transformbackplayer = GameObject.Find("pointtoclimbofAI").GetComponent<Transform>();
        transformbottom = transform.GetChild(1);
        player = GameObject.Find("CS Character Controller").GetComponent<Transform>();
        AIOnly = GameObject.Find("AI").GetComponent<NavMeshAgent>();
        AIrb = GameObject.Find("AI").GetComponent<Rigidbody>();
        AI = GameObject.Find("AI").GetComponent<Transform>();
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
                //AIOnly.Warp(transformbackplayer.position);
            }
        }
            
        if (AIclimb == true)
        {
            AIOnly.Warp( new Vector3( player.position.x, player.position.y, player.position.z - 0.83f));
            AIOnly.enabled = false;
            
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
            Invoke("teleporttoTop", 3.0f);            
        }
    }
    private void teleporttoTop()
    {
        AIOnly.Warp(transformTop.position);
        AIrb.isKinematic = true;
        AIrb.useGravity = true;
    }
}
