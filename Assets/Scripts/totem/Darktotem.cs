using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeTotem { Dark, Light}

public class Darktotem : MonoBehaviour
{
    [SerializeField] private AI_Buddy i_Buddy;
    [SerializeField] private GameObject buddy;
    public GameObject Dome;
    [SerializeField] private AI_leech[] aI_Leeches;
    private Darktotem darktotem;
    [SerializeField] private Collider totem;
    [SerializeField] public Collider totem2;
    [SerializeField] public Animator animator;
    public Interactor interactor;
    public WoodForATK1 woodForATK1;
    public openLightTotem openLight = null;
    public Light[] lightsOfTotemOpen = null;
    public typeTotem _typeTotem;

    private readonly Collider[] _colliders = new Collider[3];
    public int indexTotem;


    public LayerMask player;
    public LayerMask monster;
    public LayerMask aiBuddy;

    public float radius;
    public bool change = false;
    public int indexmonster = 0;
    public int indexarray = 0;
    public int indexAIBuddy = 0;
    public Light light;


    private void Start()
    {
        switch (_typeTotem)
        {
            case typeTotem.Dark:
                /*Collider[] hitsMonster = Physics.OverlapSphere(transform.position, radius, monster, QueryTriggerInteraction.Ignore);
                indexmonster = Physics.OverlapSphereNonAlloc(transform.position, radius, _colliders, monster);
                foreach (Collider hit in hitsMonster)
                {

                    if (hit.tag == "Monster")
                    {
                        aI_Leeches = new AI_leech[indexmonster];
                        if (indexarray < indexmonster)
                        {
                            var arrayMons = _colliders[indexarray].GetComponent<AI_leech>();
                            aI_Leeches
                            Debug.Log(_colliders[indexarray]);
                            indexarray++;
                        }

                    }
                }*/
                break;
            case typeTotem.Light:                
                    break;
        }
        
    }
    private void Awake()
    {
        interactor = GameObject.Find("CS Character Controller").GetComponent<Interactor>();
        woodForATK1 = GameObject.Find("weaponL").GetComponent<WoodForATK1>();
        i_Buddy = GameObject.Find("AI").GetComponent<AI_Buddy>();
        buddy = i_Buddy.gameObject;
        //gameObjecttotem = this.gameObject.GetComponent<GameObject>();
        totem = GetComponent<Collider>();
        totem2 = this.gameObject.transform.GetChild(1).GetComponent<Collider>();
        darktotem = GetComponent<Darktotem>();
        animator = GetComponent<Animator>();
        //totem = 
    }
    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, player, QueryTriggerInteraction.Ignore);
        //Collider[] hitsMonster = Physics.OverlapSphere(transform.position, radius, monster, QueryTriggerInteraction.Ignore);

        if( change == false)
        {
            foreach (Collider hit in hits)
            {
                Debug.Log(hit + "hit");
                if (hit.tag == "Player" && _typeTotem == typeTotem.Dark)
                {
                    if (i_Buddy.aifollow == true)
                    {
                        return;
                    }
                    Debug.Log(hit + "hit");
                    i_Buddy.aifollow = true;
                    i_Buddy.followwithtotem = true;
                    i_Buddy.currentState = new Follow_Buddy(i_Buddy.gameObject, i_Buddy.agent, i_Buddy.player, i_Buddy.animator, i_Buddy.aifollow, i_Buddy.stamina);
                }
            }
        }
        else if (change == true)
        {
            i_Buddy.aifollow = false;
            i_Buddy.followwithtotem = false;
            i_Buddy.currentState = new Idle_Buddy(i_Buddy.gameObject, i_Buddy.agent, i_Buddy.player, i_Buddy.animator, i_Buddy.aifollow, i_Buddy.stamina);
            this.darktotem.enabled = false;
        }

        checkAI();

        if(change == false)
        {
            if(light.range < 30)
            {
                light.range += 10f * Time.deltaTime;
            }
            if(light.range >= 30)
            {
                Dome.SetActive(true);
            }else
            {
                return;
            }
        }
    }

    public void hitDestroyingame()
    {
        Debug.Log("hitdestroy");
        if(change == true)
        {
            Collider[] hitsMonster = Physics.OverlapSphere(transform.position, radius, monster, QueryTriggerInteraction.Ignore);
            indexmonster = Physics.OverlapSphereNonAlloc(transform.position, radius, _colliders, monster);
            indexarray = 0;
            foreach (Collider hit in hitsMonster)
            {

                if (hit.tag == "Monster")
                {
                    
                    if (indexarray < indexmonster)
                    {
                        Debug.Log(_colliders[indexarray]);
                        var arrayMons = _colliders[indexarray].GetComponent<AI_leech>();
                        _colliders[indexarray].gameObject.SetActive(false);
                        indexarray++;
                    }

                }
            }
        }
    }

    public void checkAI()
    {
        Collider[] hitsMonster = Physics.OverlapSphere(transform.position, radius, aiBuddy, QueryTriggerInteraction.Ignore);
        indexAIBuddy = Physics.OverlapSphereNonAlloc(transform.position, radius, _colliders, aiBuddy);
        if (indexAIBuddy > 0)
        {
            //transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
            //transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);
            var arrayMons = _colliders[0].GetComponent<invisibleAI>();
            float distanceAILamp = Vector3.Distance(transform.position, arrayMons.transform.position);
            if (arrayMons.currentCutoff < 1)
            {
                arrayMons.CutofFromLighter = 0;
            }
            //Debug.Log(distanceAILamp);
            if (distanceAILamp < radius && arrayMons.follow == false && change == false)
            {
                arrayMons.neartotem = true;
                arrayMons.CutofFromLighter = (distanceAILamp / radius) - 1f;
                if (arrayMons.CutofFromLighter < 0)
                {
                    arrayMons.CutofFromLighter *= -3.0f;
                }

            }
            else if (distanceAILamp >= radius || change == true)
            {
                arrayMons.neartotem = false;

            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
     Gizmos.DrawWireSphere(transform.position, radius);
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "weapon")
        {
            Debug.Log("hitTrigger");
            animator.CrossFadeInFixedTime("TotemLightDestroy", 0.1f, 0);
            animator.CrossFadeInFixedTime("TotemLightDestroy_001", 0.1f, 1);
            if (change = false)
            {
                
            }
            change = true;
            Dome.SetActive(false);
            /*animator.GetLayerIndex("Base Layer");
            animator.SetBool("TotemLightDestroy", true);
            animator.GetLayerIndex("small");
            animator.SetBool("TotemLightDestroy_001", true);



            GameManager.Instance.sceneInfostage1.counttotemdestroy++;
        }
    }*/

    
    private void OnTriggerEnter(Collider other)
    {
        switch (_typeTotem)
        {
            case typeTotem.Dark:
                //animation
                //afterdestroy event totem
                openLight.countTotemdevilDestroy++;
                for (int i = 0; i < openLight.countTotemdevilDestroy; i++)
                {
                    if (lightsOfTotemOpen[i].enabled == true)
                        return;

                    lightsOfTotemOpen[i].enabled = true;
                }
                //afterdestroy event Monster
                for (int i = 0; i < aI_Leeches.Length; i++)
                {
                    aI_Leeches[i].enabled = false;
                }
                break;
            case typeTotem.Light:
                if (other.CompareTag("weapon"))
                {
                    Debug.Log("hitTrigger");
                    AudioManager.Instance.PlaySFX("hit");
                    AudioManager.Instance.PlaySFX("totemdestroy");
                    animator.CrossFadeInFixedTime("TotemLightDestroy", 0.1f, 0);
                    animator.CrossFadeInFixedTime("TotemLightDestroy_001", 0.1f, 1);
                    light.enabled = false;
                    light.range = 0;
                    totem.enabled = false;
                    Dome.SetActive(false);
                    woodForATK1.AfterUseItem();
                    Invoke("solvebool", 0.5f);
                    /*animator.GetLayerIndex("Base Layer");
                    animator.SetBool("TotemLightDestroy", true);
                    animator.GetLayerIndex("small");
                    animator.SetBool("TotemLightDestroy_001", true);*/



                    GameManager.Instance.sceneInfostage1.counttotemdestroy++;
                }
                break;
        }
        
    }

    public void restoreTotemForPuzzle()
    {
        animator.CrossFadeInFixedTime("TotemLightStay", 5.0f, 0);
        animator.CrossFadeInFixedTime("TotemLightStay_001", 5.0f, 1);
        totem.enabled = true;
        light.enabled = true;
        Dome.SetActive(true);
        change = false;
        
    }
    public void solvebool()
    {
        change = true;
    }
}
