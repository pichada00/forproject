using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checktrigerAI : MonoBehaviour
{
    [SerializeField] private AI_leech aI_Leech;

    private void Awake()
    {
        aI_Leech = GetComponentInParent<AI_leech>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "weapon")
        {
            Debug.Log("hitmonster");
            aI_Leech.currentState = new Stun_leech(aI_Leech.type, aI_Leech.range, aI_Leech.fieldOf, aI_Leech.gameObject, aI_Leech.agent, aI_Leech.player, aI_Leech.totem, aI_Leech.animator);
        }
    }
}
