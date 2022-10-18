using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiasGames.Components;

namespace DiasGames.Abilities
{
    [DisallowMultipleComponent]
    public class Sprint : AbstractAbility
    {

        private IMover _mover = null;
        private ICapsule _capsule = null;
        private Mover mover;

        [SerializeField] private LayerMask obstaclesMask;
        //[SerializeField] private float speed = 5f;
        //private bool shouldRun = false;

        private void Awake()
        {
            _mover = GetComponent<IMover>();
            _capsule = GetComponent<ICapsule>();
            mover = GetComponent<Mover>();
        }
        public override bool ReadyToRun()
        {
            return false;
           // return _mover.IsGrounded() && _action.sprint;
        }
        public override void OnStartAbility()
        {

            //SetAnimationState("Ground", 0.25f);
            //_mover.StopMovement();
            //_mover.isRun();
            //shouldRun = true;
        }


        public override void UpdateAbility()
        {
            //_mover.isRun();
            //if (!_action.sprint)
               // StopAbility();
        }

        public override void OnStopAbility()
        {
            //_capsule.ResetCapsuleSize();
        }
    }
}
