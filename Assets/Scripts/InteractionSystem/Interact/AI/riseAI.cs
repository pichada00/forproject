using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class riseAI : MonoBehaviour
{
    public Transform transformbackplayer;
    public Transform player;
    public Transform transformTop;

    public Transform AI;
    public NavMeshAgent AIOnly;
    public Rigidbody AIrb;

    public bool AIclimb = false;
    public float currentpointy;

    private void Awake()
    {
        transformbackplayer = GameObject.Find("pointtoclimbofAI").GetComponent<Transform>();
        player = GameObject.Find("CS Character Controller").GetComponent<Transform>();
        AIOnly = GameObject.Find("AI").GetComponent<NavMeshAgent>();
        AIrb = GameObject.Find("AI").GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(AIclimb == false)
        {
            if (currentpointy != player.position.y)
            {
                currentpointy = player.position.y;
            }
        }
        
            
        if (AIclimb == true)
        {
            AIOnly.Warp( new Vector3( player.position.x, player.position.y, player.position.z - 0.83f));
            AIOnly.enabled = false;
            //transformbackplayer.position = new Vector3(transformbackplayer.position.x, player.position.y, transformbackplayer.position.z);
            /*if(player.position.y > currentpointy)
            {
                AIOnly.transform.position = Vector3.up * Time.deltaTime;
            }else if(player.position.y < currentpointy)
            {
                AIOnly.transform.position = Vector3.down * Time.deltaTime;
            }else if (player.position.y == currentpointy)
            {
                AIOnly.transform.position = Vector3.zero;
            }*/
            
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && AIclimb == false)
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && AIclimb == true)
        {
            //playanimation
            AIclimb = false;
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
