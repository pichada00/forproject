using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class invisibleAI : MonoBehaviour
{
    public SkinnedMeshRenderer renderers;
    public NavMeshAgent AITraansform;
    public Transform PointbehindPlayer;
    public LighterSystem lighter;
    public Collider collider;
    public AI_Buddy aI;
    float currentCutoff = 0f;

    private void Awake()
    {
        //player = GameObject.Find("CS Character Controller").transform;
        renderers = this.gameObject.GetComponent<SkinnedMeshRenderer>();
        collider = this.gameObject.GetComponent<Collider>();
        AITraansform = GameObject.Find("AI").GetComponent<NavMeshAgent>();
        aI = GameObject.Find("AI").GetComponent<AI_Buddy>();
        PointbehindPlayer = GameObject.Find("aiwalkto").GetComponent<Transform>();
        lighter = GameObject.FindGameObjectWithTag("Lamp").GetComponent<LighterSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lighter.openlamb == true)
        {
            Material[] mats = renderers.materials;
            mats[0].SetFloat("_Cutoff", currentCutoff+=0.5f*Time.deltaTime);
            mats[1].SetFloat("_Cutoff", currentCutoff+=0.5f*Time.deltaTime);
            mats[2].SetFloat("_Cutoff", currentCutoff+=0.5f*Time.deltaTime);
            renderers.material = mats[0];
            renderers.material = mats[1];
            renderers.material = mats[2];
            if (currentCutoff >= 1)
            {
                aI.aifollow = false;
                aI.currentState = new Idle_Buddy(this.gameObject, aI.agent, aI.player, aI.animator, aI.aifollow);
                currentCutoff = 1f;
            }
        }
        else
        {
            Material[] mats = renderers.materials;
            mats[0].SetFloat("_Cutoff", currentCutoff -= 0.5f * Time.deltaTime);
            mats[1].SetFloat("_Cutoff", currentCutoff -= 0.5f * Time.deltaTime);
            mats[2].SetFloat("_Cutoff", currentCutoff -= 0.5f * Time.deltaTime);
            renderers.material = mats[0];
            renderers.material = mats[1];
            renderers.material = mats[2];
            if(currentCutoff <= 0)
            {
                currentCutoff = 0f;
            }
        }

        if (currentCutoff >= 1)
        {
            AITraansform.Warp(PointbehindPlayer.position);
        }
    }
}
