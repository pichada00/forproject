using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "sceneinfo", menuName = "position")]
public class SceneInfo : ScriptableObject
{
    public Vector3 currentCheckPointOfStageOne;
    public Vector3 currentCheckPointOfAI;
    public GameObject[] checkpointed;
    public GameObject[] totemDestroyed;
    public int countcheckpointed = 0;
    public int counttotemdestroy = 0;
    public bool handright;
    public bool handleft;
    public bool lamb;
    public bool weapon;
    public TypeMeleeWeapon typeMelee;
    
}
