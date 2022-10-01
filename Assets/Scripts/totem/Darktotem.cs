using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeTotem { Dark, Light}

public class Darktotem : MonoBehaviour
{
    [SerializeField] private AI_Buddy i_Buddy;
    [SerializeField] private GameObject buddy;
    [SerializeField] private AI_leech[] aI_Leeches;
    [SerializeField] private Collider totem;
    [SerializeField] public Collider totem2;
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

    private void Start()
    {
        switch (_typeTotem)
        {
            case typeTotem.Dark:
                Collider[] hitsMonster = Physics.OverlapSphere(transform.position, radius, monster, QueryTriggerInteraction.Ignore);
                indexmonster = Physics.OverlapSphereNonAlloc(transform.position, radius, _colliders, monster);
                foreach (Collider hit in hitsMonster)
                {

                    if (hit.tag == "Monster")
                    {
                        aI_Leeches = new AI_leech[indexmonster];
                        if (indexarray < indexmonster)
                        {
                            var arrayMons = _colliders[indexarray].GetComponent<AI_leech>();
                            Debug.Log(_colliders[indexarray]);
                            indexarray++;
                        }

                    }
                }
                break;
            case typeTotem.Light:                
                    break;
        }
        
    }
    private void Awake()
    {
        i_Buddy = GameObject.Find("AI").GetComponent<AI_Buddy>();
        buddy = i_Buddy.gameObject;
        //gameObjecttotem = this.gameObject.GetComponent<GameObject>();
        //totem = this.gameObject.transform.GetChild(0).GetComponent<Collider>();
        //totem2 = this.gameObject.transform.GetChild(1).GetComponent<Collider>();
        
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
                if (hit.tag == "Player")
                {
                    if (i_Buddy.aifollow == true)
                    {
                        return;
                    }
                    Debug.Log(hit + "hit");
                    i_Buddy.aifollow = true;
                    i_Buddy.followwithtotem = true;
                    i_Buddy.currentState = new Follow_Buddy(i_Buddy.gameObject, i_Buddy.agent, i_Buddy.player, i_Buddy.animator, i_Buddy.aifollow);
                }
            }
        }
        else if (change == true)
        {
            i_Buddy.aifollow = false;
            i_Buddy.followwithtotem = false;
            i_Buddy.currentState = new Idle_Buddy(i_Buddy.gameObject, i_Buddy.agent, i_Buddy.player, i_Buddy.animator, i_Buddy.aifollow);
        }
        Collider[] hitAI = Physics.OverlapSphere(transform.position, radius, aiBuddy, QueryTriggerInteraction.Ignore);
        indexAIBuddy = Physics.OverlapSphereNonAlloc(transform.position, radius, _colliders, aiBuddy);
        if (indexAIBuddy > 0)
        {
            checkAI();
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
        var arrayMons = _colliders[0].GetComponent<invisibleAI>();
        float distanceAILamp = Vector3.Distance(transform.position, arrayMons.transform.position);
        if (distanceAILamp < radius && arrayMons.follow == false)
        {
            arrayMons.CutofFromLighter = (distanceAILamp / radius) - 1f;
            if (arrayMons.CutofFromLighter < 0)
            {
                arrayMons.CutofFromLighter *= -1.0f;
            }
        }
        else if (arrayMons.follow == true)
        {
            arrayMons.CutofFromLighter = 1.0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
     Gizmos.DrawWireSphere(transform.position, radius);
    }
}
