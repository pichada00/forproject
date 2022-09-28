using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darktotem : MonoBehaviour
{
    [SerializeField] private AI_Buddy i_Buddy;
    [SerializeField] private GameObject buddy;
    [SerializeField] private AI_leech[] aI_Leeches;
    [SerializeField] private Collider totem;
    [SerializeField] public Collider totem2;
    public int indexTotem;


    public LayerMask player;
    public LayerMask monster;

    public float radius;
    public bool change = false;
    public int indexmonster = 0;

    private void Start()
    {
        Collider[] hitsMonster = Physics.OverlapSphere(transform.position, radius, monster, QueryTriggerInteraction.Ignore);
        foreach (Collider hit in hitsMonster)
        {

            if (hit.tag == "Monster")
            {
                aI_Leeches = new AI_leech[hitsMonster.Length];
                //aI_Leeches = GetComponents<AI_leech>();
                Debug.Log(hitsMonster.Length);
                if(indexmonster < hitsMonster.Length)
                {
                    //aI_Leeches[indexmonster] = hit.gameObject.GetComponents<AI_leech>();
                }
                indexmonster++;
            }
        }
    }
    private void Awake()
    {
        i_Buddy = GameObject.Find("AI").GetComponent<AI_Buddy>();
        buddy = i_Buddy.gameObject;
        //gameObjecttotem = this.gameObject.GetComponent<GameObject>();
        totem = this.gameObject.transform.GetChild(0).GetComponent<Collider>();
        totem2 = this.gameObject.transform.GetChild(1).GetComponent<Collider>();
        
        //totem = 
    }
    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, player, QueryTriggerInteraction.Ignore);
        Collider[] hitsMonster = Physics.OverlapSphere(transform.position, radius, monster, QueryTriggerInteraction.Ignore);

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
        }else if( change == true)
        {
            foreach (Collider hit in hitsMonster)
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
        
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
     Gizmos.DrawWireSphere(transform.position, radius);
    }
}
