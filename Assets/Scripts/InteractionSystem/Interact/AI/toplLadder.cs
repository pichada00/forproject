using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class toplLadder : MonoBehaviour
{
    public Transform AI;
    public NavMeshAgent AIOnly;
    public Rigidbody AIrb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //playanimation
                AIrb.isKinematic = false;
                AIrb.useGravity = false;
                AIclimb = true;
                AIOnly.Warp(transformbackplayer.position);
            }
        }*/
    }
}
