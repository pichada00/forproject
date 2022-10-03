using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "sceneinfo", menuName = "position")]
public class SceneInfo : ScriptableObject
{
    public Vector3 currentCheckPointOfStageOne;
    public Vector3 currentCheckPointOfAI;
    
}
