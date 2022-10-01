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
    public bool follow;
    //float currentCutoff = 0f;

    public float currentCutoff = 0f;
    public float CutofFromLighter = 0f;

    private void Awake()
    {
        //player = GameObject.Find("CS Character Controller").transform;
        renderers = GameObject.Find("Armature_Mesh_AI").GetComponent<SkinnedMeshRenderer>();
        //collider = this.gameObject.GetComponent<Collider>();
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
        if (lighter.openlamb == true && follow == false)
        {
            Material[] mats = renderers.materials;
            mats[0].SetFloat("_Cutoff", currentCutoff += CutofFromLighter * Time.deltaTime);
            mats[1].SetFloat("_Cutoff", currentCutoff += CutofFromLighter * Time.deltaTime);
            mats[2].SetFloat("_Cutoff", currentCutoff += CutofFromLighter * Time.deltaTime);
            renderers.material = mats[0];
            renderers.material = mats[1];
            renderers.material = mats[2];
            if (currentCutoff >= 0.8f)
            {
                aI.aifollow = false;
                aI.currentState = new Idle_Buddy(this.gameObject, aI.agent, aI.player, aI.animator, aI.aifollow);
                follow = true;
                mats[0].SetFloat("_Cutoff", 1f);
                mats[1].SetFloat("_Cutoff", 1f);
                mats[2].SetFloat("_Cutoff", 1f);
                renderers.material = mats[0];
                renderers.material = mats[1];
                renderers.material = mats[2];
            }
        }        
        
        if (lighter.openlamb == false)
        {
            follow = false;
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

        if (follow == true)
        {
            AITraansform.Warp(PointbehindPlayer.position);
        }
    }
}
