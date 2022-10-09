using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class invisibleAI : MonoBehaviour
{
    public SkinnedMeshRenderer renderersBody;
    public SkinnedMeshRenderer renderersEye;
    public NavMeshAgent AITraansform;
    public Transform PointbehindPlayer;
    public LighterSystem lighterL;
    public LighterSystem lighterR;
    public Interactor interactor;
    public Collider collider;
    public AI_Buddy aI;
    public bool follow;
    public bool neartotem;
    //float currentCutoff = 0f;

    public float currentCutoff = 0f;
    public float CutofFromLighter = 0f;

    private void Awake()
    {
        //player = GameObject.Find("CS Character Controller").transform;
        renderersBody = GameObject.Find("Cube_AI").GetComponent<SkinnedMeshRenderer>();
        renderersEye = GameObject.Find("Plane_AI").GetComponent<SkinnedMeshRenderer>();
        //collider = this.gameObject.GetComponent<Collider>();
        AITraansform = GameObject.Find("AI").GetComponent<NavMeshAgent>();
        aI = GameObject.Find("AI").GetComponent<AI_Buddy>();
        PointbehindPlayer = GameObject.Find("aiwalkto").GetComponent<Transform>();
        lighterL = GameObject.Find("lamb position L").GetComponent<LighterSystem>();
        lighterR = GameObject.Find("lamb position R").GetComponent<LighterSystem>();
        interactor = GameObject.Find("CS Character Controller").GetComponent<Interactor>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if ((lighterR.openlamb == true && follow == false) ||
            (lighterL.openlamb == true && follow == false) ||
            neartotem == true)
        {            
            increaseCutoff();
        }      
        
        if(neartotem == false)
        {
            if ((lighterL.openlamb == false) || (lighterR.openlamb == false))
            {
                follow = false;
                Material[] mats = renderersBody.materials;
                Material[] matsEye = renderersEye.materials;
                mats[0].SetFloat("Dissolve", currentCutoff -= 3.5f * Time.deltaTime);
                matsEye[0].SetFloat("Dissolve", currentCutoff += CutofFromLighter * Time.deltaTime);
                renderersBody.material = mats[0];
                renderersEye.material = matsEye[0];
                if (currentCutoff <= -0.1f)
                {
                    currentCutoff = -0.1f;
                }
            }
        }
        

        if (currentCutoff >= 1f)
        {
            AITraansform.Warp(PointbehindPlayer.position);
            aI.currentState = new Idle_Buddy(this.gameObject, aI.agent, aI.player, aI.animator, aI.aifollow, aI.stamina);
            interactor.handRight = false;
        }
    }
     
    public void increaseCutoff()
    {
        Material[] mats = renderersBody.materials;
        Material[] matsEye = renderersEye.materials;
        if(currentCutoff >= 1f)
        {
            currentCutoff = 1f;
            follow = true;
            aI.aifollow = false;
        }
        mats[0].SetFloat("Dissolve", currentCutoff += CutofFromLighter * Time.deltaTime);
        matsEye[0].SetFloat("Dissolve", currentCutoff += CutofFromLighter * Time.deltaTime);
        renderersBody.material = mats[0];
        renderersEye.material = matsEye[0];
    }
}
