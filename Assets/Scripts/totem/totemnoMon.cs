using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totemnoMon : MonoBehaviour
{
    [SerializeField] private AI_Buddy i_Buddy;

    public LayerMask player;
    public LayerMask monster;

    public float radius;
    public bool change = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        i_Buddy = GameObject.Find("AI").GetComponent<AI_Buddy>();         
    }
    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, player, QueryTriggerInteraction.Ignore);        

        if (change == false)
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
                    i_Buddy.currentState = new Follow_Buddy(i_Buddy.gameObject, i_Buddy.agent, i_Buddy.player, i_Buddy.animator, i_Buddy.aifollow,i_Buddy.stamina);
                }
            }
        }else if(change == true)
        {
            i_Buddy.aifollow = false;
            i_Buddy.followwithtotem = false;
            i_Buddy.currentState = new Idle_Buddy(i_Buddy.gameObject, i_Buddy.agent, i_Buddy.player, i_Buddy.animator, i_Buddy.aifollow, i_Buddy.stamina);
        }
    }
}
