using DiasGames.Abilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullAIAlly : AbstractAbility
{
    [SerializeField] private LayerMask pullMask;
    [SerializeField] private float globalRadiusDetection = 0.5f;
    [SerializeField] private Vector2 offsetOnLedge;

    [Header("Debug")]
    [SerializeField] private Color debugColor;
    public override void OnStartAbility()
    {
        throw new System.NotImplementedException();
    }

    public override bool ReadyToRun()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateAbility()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
